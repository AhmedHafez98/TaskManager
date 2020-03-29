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
        List<int> idxs;
        private void Form1_Load(object sender, EventArgs e)
        {
            l = new List<string>();
            idxs = new List<int>();
            for (var i = 0; i < 25; i++)
                idxs.Add(-1);
            idxs[4] = 0;
            idxs[8] = 1;
            idxs[13] = 2;
            idxs[24] = 3;
            processView();          
            l.Add("Idle");
            l.Add("Normal");
            l.Add("High");
            l.Add("RealTime");
          //  l.Add("Process Not Found");
            foreach (var i in l)
                comboBox1.Items.Add(i);
           // comboBox1.SelectedIndex = 4;
            
        }

        private void buEndTask_Click(object sender, EventArgs e)
        {
           // comboBox1.Text = "Test";
            int pid=0;
            try
            {
                pid = int.Parse(txPid.Text);
                Process p = Process.GetProcessById(pid);
                try
                {
                    p.Kill();
                }
                catch
                {
                    MessageBox.Show("This System Process");
                }
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
        void setpid(int prior)
        {
            if (prior == 4)
                comboBox1.SelectedIndex = 0;
            else if (prior == 8)
                comboBox1.SelectedIndex = 1;
            else if (prior == 13)
                comboBox1.SelectedIndex = 2;
            else if (prior == 24)
                comboBox1.SelectedIndex = 3;
            else
                comboBox1.Text= "Process Not Found";
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pid=-1;
            bool f = false;
            try
            {
                pid = int.Parse(txPid.Text);
                Process p = Process.GetProcessById(pid);
                f = true;
               // MessageBox.Show(p.BasePriority.ToString());
                var prior = comboBox1.SelectedIndex;
                if (prior == idxs[p.BasePriority])
                {

                }
                else if (prior == 0)
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
                if (pid != -1&&comboBox1.SelectedIndex!=4&&f==true)
                {
                    setpid(Process.GetProcessById(pid).BasePriority);
                    MessageBox.Show("This System Process");
                }
                else 
                {
                    
                    MessageBox.Show("This Process Not Found");
                    comboBox1.Text = "Process Not Found";
                
                }
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
                setpid(prior);
            }
            catch (Exception ex)
            {
            //    MessageBox.Show("sdf");
                comboBox1.Text = "Process Not Found";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            processView();
        }
    }
}
