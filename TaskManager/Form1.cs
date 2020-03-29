using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace TaskManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }      
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txPid.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
        }
        List<string> l;
        private void Form1_Load(object sender, EventArgs e)
        {
            l = new List<string>();
            processView();          
            l.Add("Idle");
            l.Add("Normal");
            l.Add("High");
            l.Add("RealTime");
            l.Add("Process Not Found");
            foreach (var i in l)
                comboBox1.Items.Add(i);
            comboBox1.SelectedIndex = 4;
        }

        private void buEndTask_Click(object sender, EventArgs e)
        {
            int pid=0;
            try
            {
                pid = int.Parse(txPid.Text);
                Process p = Process.GetProcessById(pid);
                p.Kill();
                processView();
            }
            catch( Exception ex)
            {
                
                MessageBox.Show("This Process Not Found");
            }
     
        }

        private void txPid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!(char.IsNumber(e.KeyChar)|| char.IsControl(e.KeyChar)))
            e.Handled = true;
        }
        void processView()
        {
            dataGridView1.Rows.Clear();
            var AllProcess = Process.GetProcesses();

            foreach (var p in AllProcess)
            {
                try
                {
                    dataGridView1.Rows.Add(p.ProcessName, p.Id, "This");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pid;
            try
            {
                pid = int.Parse(txPid.Text);
                Process p = Process.GetProcessById(pid);
                var prior = comboBox1.SelectedIndex;
                if (prior == 0)
                {
                    p.PriorityClass = ProcessPriorityClass.Idle;
                  
                }
                else if (prior == 1)
                {
                    p.PriorityClass = ProcessPriorityClass.Normal;
                  
                }
                else if (prior == 2)
                {
                    p.PriorityClass = ProcessPriorityClass.High;
                   // MessageBox.Show("Done");
                }
                else
                {
                    p.PriorityClass = ProcessPriorityClass.RealTime;
                   // MessageBox.Show("Done");
                }
            }
            catch (Exception ex)
            {
                if (comboBox1.SelectedIndex != 4)
                {
                    comboBox1.SelectedIndex = 4;
                    MessageBox.Show("This Process Not Found");
                }
            }
        }

        

        private void buchng_Click(object sender, EventArgs e)
        {
            
            int pid;
            try
            {
                pid = int.Parse(txPid.Text);
                Process p = Process.GetProcessById(pid);
              
                var prior = comboBox1.SelectedIndex;
                if (prior == 0)
                {
                    p.PriorityClass = ProcessPriorityClass.Idle;
                    MessageBox.Show("Done");
                }
                else if (prior == 1)
                {
                    p.PriorityClass = ProcessPriorityClass.Normal;
                    MessageBox.Show("Done");
                }
                else if (prior == 2)
                {
                    p.PriorityClass = ProcessPriorityClass.High;
                    MessageBox.Show("Done");
                }
                else
                {
                    p.PriorityClass = ProcessPriorityClass.RealTime;
                    MessageBox.Show("Done");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("This Process Not Found");
            }
        }

        private void txPid_TextChanged(object sender, EventArgs e)
        {
           
            int pid = 0;
            try
            {
                pid = int.Parse(txPid.Text);
                Process p = Process.GetProcessById(pid);
                var prior = int.Parse(p.BasePriority.ToString());
                if (prior == 4)
                    comboBox1.SelectedIndex = 0;
                else if (prior == 8)
                    comboBox1.SelectedIndex = 1;
                else if (prior == 13)
                    comboBox1.SelectedIndex = 2;
                else if (prior == 24)
                    comboBox1.SelectedIndex = 3;
                else
                    comboBox1.SelectedIndex = 4;
            }
            catch (Exception ex)
            {

                
            }
        }
    }
}
