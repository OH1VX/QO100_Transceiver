using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*using System;
using System.Drawing;
using System.Windows.Forms;*/

namespace trxGui
{
    public partial class Form_BeaconMonitor : Form
    {
        private Bitmap beacon_spec_bitmap = null;
        private Bitmap beacon_wf_bitmap = null;
        private int[] beacon_spec_data = new int[2000];
        private byte[] beacon_wf_data = new byte[2000 * 120];  // waterfall history
        private int wf_row_index = 0;
        private Font status_font = new Font("Verdana", 10.0f);

        public Form_BeaconMonitor()
        {
            InitializeComponent();
            timer_beacon.Start();
        }

        private void panel_beacon_spec_Paint(object sender, PaintEventArgs e)
        {
            lock (beacon_spec_data)
            {
                int width = panel_beacon_spec.Width;
                int height = panel_beacon_spec.Height;

                using (Graphics g = e.Graphics)
                {
                    // Draw background
                    g.FillRectangle(Brushes.Black, 0, 0, width, height);
                    
                    // Draw grid
                    using (Pen gridpen = new Pen(Color.DarkGray, 1))
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            int y = (height / 4) * i;
                            g.DrawLine(gridpen, 0, y, width, y);
                        }
                    }

                    // Draw spectrum
                    using (Pen specpen = new Pen(Color.LimeGreen, 1))
                    {
                        int last_x = 0, last_y = height;
                        for (int i = 0; i < beacon_spec_data.Length && i < width; i++)
                        {
                            int val = beacon_spec_data[i];
                            int y = height - (val * height / 32767);
                            if (y < 0) y = 0;
                            if (y > height) y = height;
                            
                            g.DrawLine(specpen, last_x, last_y, i, y);
                            last_x = i;
                            last_y = y;
                        }
                    }

                    // Draw center line
                    using (Pen centerpen = new Pen(Color.Yellow, 2))
                    {
                        int center_x = width / 2;
                        g.DrawLine(centerpen, center_x, 0, center_x, height);
                    }
                }
            }
        }

        private void panel_beacon_wf_Paint(object sender, PaintEventArgs e)
        {
            lock (beacon_wf_data)
            {
                int width = panel_beacon_wf.Width;
                int height = panel_beacon_wf.Height;

                using (Graphics g = e.Graphics)
                {
                    // Draw waterfall using color mapping
                    for (int row = 0; row < height && row < 120; row++)
                    {
                        int data_row = (wf_row_index - row + 120) % 120;
                        for (int col = 0; col < width && col < 2000; col++)
                        {
                            int idx = data_row * 2000 + col;
                            byte val = beacon_wf_data[idx];
                            Color col_color = ValueToColor(val);
                            using (Pen wf_pen = new Pen(col_color))
                            {
                                g.DrawLine(wf_pen, col, row, col + 1, row);
                            }
                        }
                    }
                }
            }
        }

        private Color ValueToColor(byte value)
        {
            // Color map: black -> blue -> green -> yellow -> red -> white
            if (value < 50) return Color.Black;
            if (value < 100) return Color.Blue;
            if (value < 150) return Color.Green;
            if (value < 200) return Color.Yellow;
            if (value < 230) return Color.Red;
            return Color.White;
        }

        public void UpdateBeaconData(int[] spec_data)
        {
            if (spec_data == null || spec_data.Length == 0) return;
            
            lock (beacon_spec_data)
            {
                Array.Copy(spec_data, beacon_spec_data, Math.Min(spec_data.Length, beacon_spec_data.Length));
                
                // Add to waterfall
                lock (beacon_wf_data)
                {
                    int offset = wf_row_index * 2000;
                    //for (int i = 0; i < 512 && i < spec_data.Length; i++)
                    for (int i = 0; i < 2000 && i < spec_data.Length; i++)
                    {
                        byte normalized = (byte)Math.Min(255, spec_data[i] * 255 / 32767);
                        beacon_wf_data[offset + i] = normalized;
                    }
                    wf_row_index = (wf_row_index + 1) % 120;
                }
            }
        }

        private void timer_beacon_Tick(object sender, EventArgs e)
        {
            panel_beacon_spec.Invalidate();
            panel_beacon_wf.Invalidate();
            
            // Update status label
            label_beacon_status.Text = String.Format("Beacon Offset: {0} Hz | Lock: {1} | Time: {2:HH:mm:ss}",
                statics.beaconoffset,
                statics.beaconlock ? "LOCKED" : "FREE",
                DateTime.Now);
        }

        private void Form_BeaconMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer_beacon.Stop();
        }
    }
}
