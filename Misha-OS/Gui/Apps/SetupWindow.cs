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
            this.Controls.Add(top);
            //Top2
            top2.Location = new Point(5, 40);
            top2.ForeColor = Color.White;
            top2.Text = "";
            this.Controls.Add(top2);
            //hardDisksPanel
            hardDisksPanel.Location = new Point(5, 60);
            hardDisksPanel.Size = new Size(Display.ScreenWidth - 10, Display.ScreenHeight - 180);
            hardDisksPanel.BackgroundColor = Color.SteelBlue;
            hardDisksPanel.Visible = false;
            this.Controls.Add(hardDisksPanel);

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
                top2.Text = "Press next to contuine";
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
                    Control driveInfo = new Control();
                    driveInfo.Location = new Point(hardDisksPanel.Location.X, DrivesY);

                    //Drive name/index label
                    Label driveName = new Label();
                    driveName.Text = $"Drive {i}: " + label;
                    driveName.ForeColor = Color.White;
                    driveName.BackgroundColor = Color.SteelBlue;
                    driveName.Location = new Point(hardDisksPanel.Location.X, DrivesY);

                    driveInfo.Controls.Add(driveName);

                    //Select button
                    Button select = new Button();
                    select.Text = "Select";
                    select.BackgroundColor = Color.WhiteSmoke;
                    select.Size = new Size(100, 20);
                    select.Location = new Point(Display.ScreenWidth - 105, DrivesY);
                    select.Tag = drive;
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
                    error = "No hard drives detected.";
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

                try
                {
                    Kernel.FS.Format(SelectedDriveNum.ToString(), "FAT32", true);
                    top2.Text = "20% complete - Creating Files";
                    this.DrawAll();

                    Kernel.FS.CreateFile(SelectedDriveNum + @":\installed.bif");
                    Kernel.FS.SetFileSystemLabel(SelectedDriveNum.ToString(), "MishaOS");
                    top2.Text = "40% complete - Creating Files";
                    this.DrawAll();

                    Kernel.FS.CreateDirectory(SelectedDriveNum.ToString() + @":\System");
                    Cosmos.HAL.Global.PIT.Wait(5000);
                    top2.Text = "99% complete - Creating Files";
                    this.DrawAll();
                    Cosmos.HAL.Global.PIT.Wait(5000);
                    SetStage(4);
                }
                catch (Exception ex)
                {
                    error = ex.Message;
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
                top.Text = "Error during Setup: " + error;
                top2.Text = "Please restart your computer.";
                next.Visible = false;
                next.Text = "";
                //Fix drawing issues
                this.DrawAll();
            }
        }
    }
}
