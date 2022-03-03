using Crystalshire.Model.Forms;
using Crystalshire.Model.Models;
using Crystalshire.Model.Serialization;

namespace Crystalshire.Model {
    public partial class FormMain : Form {
        public Class? Model { get; set; }

        private FormDirection attack;
        private FormDirection death;
        private FormDirection gathering;
        private FormDirection idle;
        private FormDirection running;
        private FormDirection ressurrection;
        private FormDirection talking;
        private FormDirection walking;

        private string path = string.Empty;

        public FormMain() {
            InitializeComponent();

            Model = new Class();

            attack = new FormDirection(Model.Attack, true);
            death = new FormDirection(Model.Death, true);
            gathering = new FormDirection(Model.Gathering, true);
            idle = new FormDirection(Model.Idle, true);
            running = new FormDirection(Model.Running, true);
            ressurrection = new FormDirection(Model.Ressurrection, true);
            talking = new FormDirection(Model.Talk, true); ;
            walking = new FormDirection(Model.Walking, true);

            attack.Close();
            death.Close();
            gathering.Close();
            idle.Close();
            running.Close();
            ressurrection.Close();
            talking.Close();
            walking.Close();

            Model = null;
        }

        #region Menu File

        private void MenuNew_Click(object sender, EventArgs e) {
            if (Model is not null) {
                if (MessageBox.Show(this, "Do you want to save?", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    if (string.IsNullOrEmpty(path)) {
                        var dialog = new SaveFileDialog() {
                            Filter = "Engine Character (*.egc) | *.egc"
                        };
                       
                        if (dialog.ShowDialog() == DialogResult.OK) {
                            SaveProject(dialog.FileName);
                        }
                    }
                    else {
                        SaveProject(path);
                    }
                }
            }

            Model = new Class();

            path = string.Empty;

            TextModelId.Text = "0";
            TextProjectName.Text = string.Empty;

            SetEnabled(true);
        }

        private void MenuClose_Click(object sender, EventArgs e) {
            if (Model is not null) {
                if (MessageBox.Show(this, "Do you want to save?", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    if (string.IsNullOrEmpty(path)) {
                        var dialog = new SaveFileDialog() {
                            Filter = "Engine Character (*.egc) | *.egc"
                        };

                        if (dialog.ShowDialog() == DialogResult.OK) {
                            SaveProject(dialog.FileName);
                        }
                    }
                    else {
                        SaveProject(path);
                    }
                }
            }

            Model = null;

            path = string.Empty;

            TextModelId.Text = "0";
            TextProjectName.Text = string.Empty;

            SetEnabled(false);
        }

        private void MenuOpen_Click(object sender, EventArgs e) {
            if (Model is not null) {
                if (MessageBox.Show(this, "Do you want to save?", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    if (string.IsNullOrEmpty(path)) {
                        var save = new SaveFileDialog() {
                            Filter = "Engine Character (*.egc) | *.egc"
                        };

                        if (save.ShowDialog() == DialogResult.OK) {
                            SaveProject(save.FileName);
                        }
                    }
                    else {
                        SaveProject(path);
                    }
                }
            }

            var open = new OpenFileDialog() {
                Filter = "Engine Character (*.egc) | *.egc",
                Multiselect = false,
                CheckFileExists = true,
                CheckPathExists = true
            };

            if (open.ShowDialog() == DialogResult.OK) {
                path = open.FileName;

                LoadProject(path);

                SetEnabled(true);
            }
        }

        private void MenuSave_Click(object sender, EventArgs e) {
            if (Model is not null) {
                if (string.IsNullOrEmpty(path)) {
                    var dialog = new SaveFileDialog() {
                        Filter = "Engine Character (*.egc) | *.egc"
                    };

                    if (dialog.ShowDialog() == DialogResult.OK) {
                        path = dialog.FileName;

                        SaveProject(dialog.FileName);

                    }
                }
                else {
                    SaveProject(path);
                }
            }
        }

        private void MenuSaveNew_Click(object sender, EventArgs e) {
            if (Model is not null) {
                var dialog = new SaveFileDialog() {
                    Filter = "Engine Character (*.egc) | *.egc"
                };

                if (dialog.ShowDialog() == DialogResult.OK) {
                    SaveProject(dialog.FileName);
                }
            }
        }

        private void MenuExit_Click(object sender, EventArgs e) {
            Close();
        }

        private void CloseProgram() {
            if (Model is not null) {
                if (MessageBox.Show(this, "Do you want to save?", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    if (string.IsNullOrEmpty(path)) {
                        var dialog = new SaveFileDialog() {
                            Filter = "Engine Character (*.egc) | *.egc"
                        };

                        if (dialog.ShowDialog() == DialogResult.OK) {
                            SaveProject(dialog.FileName);
                        }
                    }
                    else {
                        SaveProject(path);
                    }
                }
            }
        }

        #endregion

        #region Menu Animation

        private void MenuAttack_Click(object sender, EventArgs e) {
            if (attack.IsDisposed) {
                attack = new FormDirection(Model!.Attack, true);
            }

            attack.Show();
        }

        private void MenuDeath_Click(object sender, EventArgs e) {
            if (death.IsDisposed) {
                death = new FormDirection(Model!.Death, true);
            }

            death.Show();
        }

        private void MenuGathering_Click(object sender, EventArgs e) {
            if (gathering.IsDisposed) {
                gathering = new FormDirection(Model!.Gathering, true);
            }

            gathering.Show();
        }

        private void MenuIdle_Click(object sender, EventArgs e) {
            if (idle.IsDisposed) {
                idle = new FormDirection(Model!.Idle, true);
            }

            idle.Show();
        }

        private void MenuRunning_Click(object sender, EventArgs e) {
            if (running.IsDisposed) {
                running = new FormDirection(Model!.Running, true);
            }

            running.Show();
        }

        private void MenuRessurrection_Click(object sender, EventArgs e) {
            if (ressurrection.IsDisposed) {
                ressurrection = new FormDirection(Model!.Ressurrection, true);
            }

            ressurrection.Show();
        }

        private void MenuTalk_Click(object sender, EventArgs e) {
            if (talking.IsDisposed) {
                talking = new FormDirection(Model!.Talk, true);
            }

            talking.Show();
        }

        private void MenuWalking_Click(object sender, EventArgs e) {
            if (walking.IsDisposed) {
                walking = new FormDirection(Model!.Walking, true);
            }

            walking.Show();
        }

        #endregion

        #region Menu Emote

        private void MenuEmote_Click(object sender, EventArgs e) {

        }

        #endregion

        #region Special

        private void MenuSpecial_Click(object sender, EventArgs e) {

        }

        #endregion

        private void SaveProject(string file) {
            if (Model is not null) {
                var project = new Project();
                project.Save(Model, file);
            }
        }

        private void LoadProject(string file) {
            var project = new Project();
            Model = project.Load(file);

            if (Model is not null) {
                TextModelId.Text = Model.Id.ToString();
                TextProjectName.Text = Model.Name;
            }
        }

        private void SetEnabled(bool enabled) {
            TextModelId.Enabled = enabled;
            TextProjectName.Enabled = enabled;
            TextPassphrase.Enabled = enabled;

            MenuSave.Enabled = enabled;
            MenuSaveNew.Enabled = enabled;
            MenuClose.Enabled = enabled;

            MenuAnimation.Enabled = enabled;
            MenuExport.Enabled = enabled;
        }

        private void TextModelId_TextChanged(object sender, EventArgs e) {
            if (Model is not null) {
                Model.Id = Util.GetValue((TextBox)sender);
            }
        }

        private void TextProjectName_TextChanged(object sender, EventArgs e) {
            if (Model is not null) {
                Model.Name = ((TextBox)sender).Text;
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            CloseProgram();
        }

        private void MenuExport_Click(object sender, EventArgs e) {
            if (Model is null) {
                return;
            }

            var passphrase = TextPassphrase.Text.Trim();

            if (string.IsNullOrEmpty(passphrase)) {
                MessageBox.Show("You must use password to export model.");
            } 
            else {
                var save = new SaveFileDialog() {
                    Filter = "Crystalshire Character (*.csc) | *.csc"
                };

                if (save.ShowDialog() == DialogResult.OK) {
                    var export = new Export() {
                        Passphrase = passphrase
                    };

                    export.Save(Model!, save.FileName);
                }
            }
        }
    }
}