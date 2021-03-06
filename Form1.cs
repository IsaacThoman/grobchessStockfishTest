using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess2
{



    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            board.resetBoard();

        }
        public static int groupBox1Y = 0;
        public static int groupBox2Y = 0;
        public static int groupBox3Y = 0;
        public static int groupBox4Y = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1Y = onlineSettingsGroup.Location.Y;
            groupBox2Y = debugGroupBox.Location.Y;
            rpc.setRPC("Playing locally", "", "1", "❤️");

            if (chess2.Properties.Settings.Default.userid == 1111)
            {
                Random r2 = new Random();
                int rInt2 = r2.Next(1000, 9999);
                chess2.Properties.Settings.Default.userid = rInt2;
                Properties.Settings.Default.Save();
            }
            beanchat.userID = chess2.Properties.Settings.Default.userid;
            userIDLabel.Text = "Your ID: "+beanchat.userID.ToString();
            //     renderer.render(panel1);

            renderer.setTheme(chess2.Properties.Settings.Default.theme);

            switch (chess2.Properties.Settings.Default.theme)
            {
                case 0:
                    themeDefaultRadioBtn.Checked = true;
                    break;
                case 1:
                    themeGreenRadioBtn.Checked = true;
                    break;
                case 2:
                    themeIcySeaRadioBtn.Checked = true;
                    break;

            }
            if (chess2.Properties.Settings.Default.darkMode)
            {
                darkThemeRadio.Checked = true;
                setWindowTheme(System.Drawing.ColorTranslator.FromHtml("#2b2c38"), SystemColors.Control);

            }
            else
            {
                setWindowTheme(SystemColors.Control, SystemColors.ControlText);
                lightThemeRadio.Checked = true;

            }
            if (chess2.Properties.Settings.Default.showMoves)
            {
                showLegalMovesRadio.Checked = true;
            }
            else
            {
                showLegalMovesRadio.Checked = false;
            }
            renderer.showLegalMoves = chess2.Properties.Settings.Default.showMoves;

                renderer.copyBoard();
            gamePictureBox.Image = renderer.renderBitmap();
            capturedPiecesPictureBox.Image = renderer.renderCapturesBar();
           // capturedPiecesPictureBox.Image = renderer.piece[0];
        }



        private void panel1_Click(object sender, EventArgs e)
        {

            

       //     var relativePoint = this.PointToClient(Cursor.Position);

        //    int selX =  (relativePoint.X-panel1.Location.X) / 64 + 1;
          //  int selY = 0 - (relativePoint.Y - panel1.Location.Y) / 64 + 8;
           
         //   Interface.setSelection(selX, selY);

            //  renderer.render(panel1);
                 // pictureBox3.Image = renderer.renderBitmap();

        }

        private void Form1_Click(object sender, EventArgs e)
        {
            Interface.setSelection(0, 0);
                
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
            if (radioLocal.Checked)
            {
                rpc.setDetails("Playing locally");
                Interface.againstBot = false;
                Interface.againstLocal = true;
                Interface.againstOnline = false;
                Interface.whitesMove = true;
                timer1.Enabled = false;
                onlineSettingsGroup.Visible = false;
                onlineSettingsGroup.Location = new Point(onlineSettingsGroup.Location.X, groupBox2Y);
                debugGroupBox.Location = new Point(debugGroupBox.Location.X, groupBox1Y);

                board.resetBoard();

                //     renderer.render(panel1);
                gamePictureBox.Image = renderer.renderBitmap();
                capturedPiecesPictureBox.Image = renderer.renderCapturesBar();
            }

        }

        private void radioComputer_CheckedChanged(object sender, EventArgs e)
        {
            if (radioComputer.Checked)
            {
                rpc.setDetails("Playing against computer");
                Interface.againstBot = true;
                Interface.againstLocal = false;
                Interface.againstOnline = false;
                Interface.whitesMove = true;
                timer1.Enabled = false;
                onlineSettingsGroup.Visible = false;
                onlineSettingsGroup.Location = new Point(onlineSettingsGroup.Location.X, groupBox2Y);
                debugGroupBox.Location = new Point(debugGroupBox.Location.X, groupBox1Y);
                board.resetBoard();
                //   renderer.render(panel1);
                //   pictureBox3.Image = renderer.renderBitmap();
            }


        }

        private void radioOnline_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOnline.Checked) {
                rpc.setDetails("Playing online");
                Interface.againstBot = false;
                Interface.againstLocal = false;
                Interface.againstOnline = true;
                timer1.Enabled = true;
                onlineSettingsGroup.Visible = true;
                onlineSettingsGroup.Location = new Point(onlineSettingsGroup.Location.X, groupBox1Y);
                debugGroupBox.Location = new Point(debugGroupBox.Location.X, groupBox2Y);
                beanchat.pullBoard();
                //  renderer.render(panel1);
                //          pictureBox3.Image = renderer.renderBitmap();
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = beanchat.createBoardString(board.boardSquare, Interface.whitesMove);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            board.boardSquare=beanchat.createBoardFromString(textBox1.Text);
            Interface.whitesMove = beanchat.whitesMoveRecieved;
            //  renderer.render(panel1);
   //         pictureBox3.Image = renderer.renderBitmap();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            beanchat.channelSet = (int)numericUpDown1.Value;
            beanchat.pullBoard();
            
            animation = renderer.renderBitmapAnimation();
                animationTimer.Enabled = true;

            //  renderer.render(panel1);
            //        pictureBox3.Image = renderer.renderBitmap();

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            beanchat.channelSet = (int)numericUpDown1.Value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Interface.whitesMove = true;

            if (Interface.againstOnline)
            {
                beanchat.send("w43256234111111110000000000000000000000000000000077777777a98bc89aSTOP", beanchat.channelSet);
            }
            else
            {
                board.resetBoard();
            }
            //     renderer.render(panel1);
 //           pictureBox3.Image = renderer.renderBitmap();

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (themeIcySeaRadioBtn.Checked)
            {
                renderer.setTheme(2);
                chess2.Properties.Settings.Default.theme = 2;
                Properties.Settings.Default.Save();
                gamePictureBox.Image = renderer.renderBitmap();
                capturedPiecesPictureBox.Image = renderer.renderCapturesBar();

            }

        }
        

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (themeGreenRadioBtn.Checked)
            {
                renderer.setTheme(1);
                chess2.Properties.Settings.Default.theme = 1;
                Properties.Settings.Default.Save();
                gamePictureBox.Image = renderer.renderBitmap();
                capturedPiecesPictureBox.Image = renderer.renderCapturesBar();
            }

        }






        //
        public static Bitmap[] animation = new Bitmap[11];
        public static int animationFrame = 1;

        private void button4_Click(object sender, EventArgs e)
        {
            animationTimer.Enabled = true;
            //    pictureBox2.Image = renderer.renderBitmap();
            //    Bitmap[] test = renderer.renderBitmapAnimation();
            //   animation = renderer.renderBitmapAnimation();
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            if (animationFrame >= 10) { 
                animationFrame = 0; 
                animationTimer.Enabled = false;
                
                gamePictureBox.Image = renderer.renderBitmap();
                capturedPiecesPictureBox.Image = renderer.renderCapturesBar();
                return;
            }


            animationFrame+=1;
            gamePictureBox.Image = animation[animationFrame];
          //  capturedPiecesPictureBox.Image = renderer.renderCapturesBar();   that's stupid
        }






        private void pictureBox3_Click(object sender, EventArgs e)
        {

            var relativePoint = this.PointToClient(Cursor.Position);

            int selX = (relativePoint.X - gamePictureBox.Location.X) / (gamePictureBox.Width / 8) + 1;
            int selY = 0 - (relativePoint.Y - gamePictureBox.Location.Y) / (gamePictureBox.Height / 8) + 8;
            if (Interface.reverseForBlackPlayer)
            {
                selX = 9 - selX;
                selY = 9 - selY;
            }

            if (Interface.againstBot)
            {
                Interface.setSelection(selX, selY);
                if (Interface.firstSel == true)
                {
                    if (Interface.againstBot)
                    {
                        if (Interface.newAnimation)
                        {
                            animationTimer.Enabled = true;
                            Interface.newAnimation = false;
                        }
                        else
                        {
                            gamePictureBox.Image = renderer.renderBitmap();
                            capturedPiecesPictureBox.Image = renderer.renderCapturesBar();
                        }
                    }
                    else
                    {
                        animationTimer.Enabled = true;
                    }
                        

                    

                }
                else
                {
                    gamePictureBox.Image = renderer.renderBitmap();
                    capturedPiecesPictureBox.Image = renderer.renderCapturesBar();
                }
                
            }
            else
            {
                Interface.setSelection(selX, selY);
                if (Interface.firstSel == true)
                {
                    animation = renderer.renderBitmapAnimation();
                    animationTimer.Enabled = true;
                }
                else
                {
                    gamePictureBox.Image = renderer.renderBitmap();
                    capturedPiecesPictureBox.Image = renderer.renderCapturesBar();
                }
            }

            
         //   gamePictureBox.Image =renderer.renderBitmap();

        }

        private void animTestCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void themeDefaultRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (themeDefaultRadioBtn.Checked)
            {
                renderer.setTheme(0);
                chess2.Properties.Settings.Default.theme = 0;
                Properties.Settings.Default.Save();
                gamePictureBox.Image = renderer.renderBitmap();
                capturedPiecesPictureBox.Image = renderer.renderCapturesBar();
            }
        }

        private void capturedPiecesPictureBox_Click(object sender, EventArgs e)
        {
            capturedPiecesPictureBox.Image =  renderer.renderCapturesBar();
        }

      
        public static Form firstForm = Form1.ActiveForm;

        private void designBtn_Click(object sender, EventArgs e)
        {
           // AACheckBox.Checked = false;
            Form form2 = new designer();
            firstForm = Form1.ActiveForm;
            Form1.ActiveForm.Hide();
            form2.Show();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            autoWindowSize();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            setSize((int)numericUpDown2.Value);
            autoWindowSize();
        }
        public static int baseResolution = 512;
        void setSize(int newRes)
        {
            baseResolution = newRes;
            if (AACheckBox.Checked) {
                renderer.resolution = (int)(AArenderScale);
            }
            else
            {
                renderer.resolution = newRes;
            }

            gamePictureBox.Width = newRes;
            gamePictureBox.Height = newRes;
            capturedPiecesPictureBox.Width = (int)(newRes / 8);
            capturedPiecesPictureBox.Height = newRes;
            gamePictureBox.Image = renderer.renderBitmap();
        }

         void autoWindowSize()
        {
            capturedPiecesPictureBox.Location = new Point(18 + gamePictureBox.Width, 26);
            int groupBoxX = capturedPiecesPictureBox.Location.X + capturedPiecesPictureBox.Width + 8;
            //   groupBox1.Location = new Point(groupBoxX, 26);
            //   groupBox2.Location = new Point(groupBoxX, 144);
            //   groupBox4.Location = new Point(groupBoxX, 420);
            //   onlineSettingsGroup.Location = new Point(groupBoxX, 308);
            groupBox1.Location = new Point(groupBoxX, groupBox1.Location.Y);
            groupBox2.Location = new Point(groupBoxX, groupBox2.Location.Y);
            debugGroupBox.Location = new Point(groupBoxX, debugGroupBox.Location.Y);
            onlineSettingsGroup.Location = new Point(groupBoxX, onlineSettingsGroup.Location.Y);

            int formHeight = (int)((gamePictureBox.Height + gamePictureBox.Location.Y) * 1.1);
            if(formHeight< capturedPiecesPictureBox.Height) { formHeight = (int)(capturedPiecesPictureBox.Height * 1.2); }
            int formWidth = (int)((gamePictureBox.Width + capturedPiecesPictureBox.Width + groupBox1.Width) * 1.1);
            this.Size = new Size(formWidth, formHeight);
        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                numericUpDown2.Visible = true;
                button6.Visible = true;
            }
            else
            {
                numericUpDown2.Visible = false;
                button6.Visible = false;
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {
                setSize(512);
                autoWindowSize();
            }

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked) {
                setSize(768);
                autoWindowSize();
            }

        }
        public static int AArenderScale = 768;
        private void AACheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AACheckBox.Checked)
            {
                renderer.resolution = (int)(AArenderScale);
            }
            else
            {
                renderer.resolution = gamePictureBox.Width;
            }
            gamePictureBox.Image = renderer.renderBitmap();
            
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            AArenderScale = (int)numericUpDown3.Value;
            if (AACheckBox.Checked)
            {
                renderer.resolution = (int)( AArenderScale);
            }
            else
            {
                renderer.resolution = baseResolution;
            }
            gamePictureBox.Image = renderer.renderBitmap();
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (lightThemeRadio.Checked)
            {
                setWindowTheme(SystemColors.Control,SystemColors.ControlText);
                chess2.Properties.Settings.Default.darkMode = false;
                Properties.Settings.Default.Save();
                capturedPiecesPictureBox.Image = renderer.renderCapturesBar();
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (darkThemeRadio.Checked)
            {
                setWindowTheme(System.Drawing.ColorTranslator.FromHtml("#2b2c38"), SystemColors.Control);
                chess2.Properties.Settings.Default.darkMode = true;
                Properties.Settings.Default.Save();
                capturedPiecesPictureBox.Image = renderer.renderCapturesBar();
            }
        }
        void setWindowTheme(Color backColor, Color foreground1)
        {
            this.BackColor = backColor;
            groupBox1.ForeColor = foreground1;
            groupBox2.ForeColor = foreground1;
            groupBox3.ForeColor = foreground1;
            groupBox6.ForeColor = foreground1;
            groupBox5.ForeColor = foreground1;
            onlineSettingsGroup.ForeColor = foreground1;
            debugGroupBox.ForeColor = foreground1;
        }

        private void showLegalMovesRadio_CheckedChanged(object sender, EventArgs e)
        {
            chess2.Properties.Settings.Default.showMoves = showLegalMovesRadio.Checked;
            renderer.showLegalMoves = showLegalMovesRadio.Checked;
            gamePictureBox.Image = renderer.renderBitmap();
            Properties.Settings.Default.Save();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Interface.reverseForBlackPlayer = checkBox1.Checked;
            gamePictureBox.Image= renderer.renderBitmap();
        }
    }
}
