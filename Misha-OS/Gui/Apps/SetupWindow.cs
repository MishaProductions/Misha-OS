using Cosmos.Core;
using Cosmos.HAL;
using Cosmos.System.FileSystem.Listing;
using MishaOS.Drivers;
using MishaOS.Gui.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Gui.Windows
{
    public class SetupWindow : Window
    {
        Button next = new Button();
        Label top = new Label();
        Label top2 = new Label();
        Control hardDisksPanel = new Control();
        string stackTrace = "";
        string errormsg = "";
        /// <summary>
        /// The stage varible.
        /// Stage 0 = Welcome Screen
        /// Stage 1 = Select Disk
        /// Stage 2 = Confirm
        /// Stage 3 = Installing
        /// Stage 4 = Finish Screen
        /// Stage 5 = Reboot
        /// Stage 6 = Error (normally not visisble)
        /// </summary>
        int stage = 0;
        /// <summary>
        /// The selected drive number
        /// </summary>
        int SelectedDriveNum = 0;

        string error;
        public SetupWindow()
        {
            //Init Window
            this.Text = "Setup";
            this.BackgroundColor = Color.SteelBlue;
            this.ShouldDrawCloseButton = false;
            this.Size = new Size(Display.ScreenWidth, Display.ScreenHeight);
            this.WindowMoveable = false;

            //Next
            next.Text = "Next";
            next.Size = new Size(100, 20);
            next.Location = new Point(Display.ScreenWidth - 100, Display.ScreenHeight - 20);
            next.OnClick += Next_OnClick;
            next.Visible = true;
            this.Controls.Add(next);

            //Top
            top.Location = new Point(5, 20);
            top.ForeColor = Color.White;
            top.Text = "";
            top.BackgroundColor = Color.Transparent;
            this.Controls.Add(top);
            //Top2
            top2.Location = new Point(5, 40);
            top2.ForeColor = Color.White;
            top2.Text = "";
            top2.BackgroundColor = Color.Transparent;
            this.Controls.Add(top2);
            //hardDisksPanel
            hardDisksPanel.Visible = false;
            hardDisksPanel.BackgroundColor = Color.Transparent;
            hardDisksPanel.Location = new Point(5, 60);
            hardDisksPanel.Size = new Size(Display.ScreenWidth - 10, Display.ScreenHeight - 180);
            this.Controls.Add(hardDisksPanel);
            this.DrawAll();

            //Set stage to 0. (welcome screen)
            SetStage(0);
        }

        private void Next_OnClick(object sender, EventArgs e)
        {
            SetStage(stage + 1);
        }

        public void SetStage(int newStage)
        {
            this.stage = newStage;

            if (newStage == 0)
            {
                top.Text = "Welcome to MishaOS, a operating system made by Misha using Cosmos.";
                top2.Text = "Press next to continue";
                next.Visible = true;
                //Fix drawing issues
                this.DrawAll();
            }
            else if (newStage == 1)
            {
                top.Text = "Please select a hard disk by pressing the select button.";
                top2.Text = "";
                next.Visible = false;
                hardDisksPanel.Controls.Clear();
                hardDisksPanel.Visible = true;
                int i = 0;


                int DrivesY = 60;
                foreach (var drive in Kernel.FS.GetVolumes())
                {
                    string label = drive.mName;
                    try
                    {
                        label = Kernel.FS.GetFileSystemLabel(i.ToString());
                    }
                    catch { }
                    Control driveInfo = new Control
                    {
                        Location = new Point(hardDisksPanel.Location.X, DrivesY),
                        BackgroundColor = Color.Transparent
                    };

                    //Drive name/index label
                    Label driveName = new Label
                    {
                        Text = $"Drive {i}: " + label,
                        ForeColor = Color.White,
                        BackgroundColor = Color.Transparent,
                        Location = new Point(hardDisksPanel.Location.X, DrivesY)
                    };

                    driveInfo.Controls.Add(driveName);

                    //Select button
                    Button select = new Button
                    {
                        Text = "Select",
                        BackgroundColor = Color.WhiteSmoke,
                        Size = new Size(100, 20),
                        Location = new Point(Display.ScreenWidth - 105, DrivesY),
                        Tag = drive
                    };
                    select.OnClick += delegate (object s, EventArgs e)
                    {
                        SelectedDriveNum = Convert.ToInt32((select.Tag as DirectoryEntry).mName[0].ToString());
                        SetStage(2);
                    };

                    driveInfo.Controls.Add(select);
                    //Add the control
                    hardDisksPanel.Controls.Add(driveInfo);
                    i++;
                    DrivesY += 20;
                }
                hardDisksPanel.DrawAll();
                //Fix drawing issues
                this.DrawAll();


                if (i == 0)
                {
                    error = "No hard drives detected. Please make sure that you have an PIX4 IDE controller.";
                    SetStage(6);
                }
            }
            else if (newStage == 2)
            {
                top.Text = "Warning, the drive " + SelectedDriveNum + @":\ Will be formated";
                top2.Text = "and all data will be gone. Press next to confirm.";
                next.Visible = true;
                hardDisksPanel.Controls.Clear();
                hardDisksPanel.Visible = false;
                //Fix drawing issues
                this.DrawAll();
            }
            else if (newStage == 3)
            {
                top.Text = "Installing MishaOS";
                top2.Text = "0% complete - Formating";
                next.Visible = false;
                //Fix drawing issues
                this.DrawAll();
                Display.Render(); //update display because this takes awhile

                try
                {
                    stackTrace = "At Kernel.FS.Format()\n At SetupWindow::SetStage(). Varibles: SelectedDriveNum=" + SelectedDriveNum.ToString();

                    Kernel.FS.Format(SelectedDriveNum.ToString() + @":\", "FAT32", true);


                    //Set progress
                    top2.Text = "50% complete - Copying Files";
                    //Update the screen
                    this.DrawAll();
                    Display.Render();
                    //Set the stack trace
                    stackTrace = "At SetupWindow::SetStage() (Creating files)";

                    //Create system files and folders
                    Kernel.FS.SetFileSystemLabel(SelectedDriveNum.ToString() + @":\", "MishaOS");
                    Kernel.FS.CreateFile(SelectedDriveNum + @":\MishaOS\system.cfg");
                    Kernel.FS.CreateDirectory(SelectedDriveNum.ToString() + @":\MishaOS");

                    //Set progress
                    top2.Text = "80% complete - Copying Files";
                    //Update the screen
                    this.DrawAll();
                    Display.Render();
                    //Set the stack trace
                    stackTrace = "At SetupWindow::SetStage() (part=2)";
                    Cosmos.HAL.Global.PIT.Wait(5000);


                    top2.Text = "99% complete - Creating Files";
                    this.DrawAll();
                    Display.Render();
                    Cosmos.HAL.Global.PIT.Wait(5000);
                    SetStage(4);
                }
                catch (Exception ex)
                {
                    errormsg = ex.Message;
                    SetStage(6);
                }
            }
            else if (newStage == 4)
            {
                top.Text = "MishaOS Setup Complete";
                top2.Text = "Press finish to restart.";
                next.Visible = true;
                next.Text = "Finish";
                //Fix drawing issues
                this.DrawAll();
            }
            else if (newStage == 5)
            {
                Power.CPUReboot();
                Power.ACPIReboot();
            }
            else if (newStage == 6)
            {
                top.Text = "Error during Setup: " + errormsg;
                top2.Text = "reset your computer. Stack trace: " + stackTrace;
                next.Text = "Reboot";
                //Fix drawing issues
                this.DrawAll();
                newStage = 4; //hack when user presses next computer reboots
            }
        }
    }
}
