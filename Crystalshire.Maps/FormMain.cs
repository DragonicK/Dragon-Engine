using Crystalshire.Core.Cryptography;

using Crystalshire.Maps.Model;
using Crystalshire.Maps.Common;
using Crystalshire.Maps.Editor;
using Crystalshire.Maps.Images;

namespace Crystalshire.Maps;

public partial class FormMain : Form {
    public JetBrainsMono JetBrainsMono { get; set; }
    public IGrid Grid { get; set; }
    public ITileset Tiles { get; set; }
    public IList<IMap> Maps { get; set; }
    public Colors Colors { get; set; }
    public Bitmap TextureDirection { get; set; }
    public Point[] DirectionPosition { get; set; }
    public int SelectedMapIndex { get; set; } = NoMapSelected;
    public string SelectedTileset { get; set; } = string.Empty;
    public bool IsTileClickPressed { get; set; }
    public bool IsMapClickPressed { get; set; }
    public Rectangle SelectedTileRectangle { get; set; }

    private const int NoMapSelected = -1;
    private const int DirectionImageSize = 8;

    public FormMain() {
        InitializeComponent();
        InitializeButton();
    }

    private void ChangeFont(Control control) {
        var controls = control.Controls;

        ChangeFontStye(control);

        foreach (Control _control in controls) {
            ChangeFontStye(_control);
        }
    }

    private void ChangeFontStye(Control control) {
        control.Font = JetBrainsMono.GetFont(FontStyle.Regular);
    }

    #region Project 

    private string[]? GetFilesToOpen() {
        var dialog = new OpenFileDialog() {
            InitialDirectory = Application.StartupPath + "/Projects/",
            Filter = "Engine Maps (*.mps) | *.mps",
            CheckFileExists = true,
            CheckPathExists = true,
            Multiselect = true,
            FilterIndex = 0
        };

        var result = dialog.ShowDialog();

        if (result == DialogResult.OK) {
            return dialog.FileNames;
        }

        return null;
    }

    private string? GetPathToSave() {
        var dialog = new SaveFileDialog() {
            InitialDirectory = Application.StartupPath + "/Projects/",
            Filter = "Engine Maps (*.mps) | *.mps",
            FilterIndex = 0
        };

        var result = dialog.ShowDialog();

        if (result == DialogResult.OK) {
            return dialog.FileName;
        }

        return null;
    }

    private void CloseProject() {
        const string Message = "Deseja salvar?";
        const string Title = "Aviso";

        var result = MessageBox.Show(Message, Title, MessageBoxButtons.YesNo);

        if (result == DialogResult.Yes) {
            SaveProject();
            RemoveMap();
        }
        else {
            RemoveMap();
        }
    }

    private void RemoveMap() {
        var map = Maps[SelectedMapIndex];

        Maps.Remove(map);

        TabMaps.TabPages.RemoveAt(SelectedMapIndex);

        SelectedMapIndex = TabMaps.SelectedIndex;

        PictureMap.Invalidate();
    }

    private void OpenProject() {
        var files = GetFilesToOpen();

        if (files != null) {
            foreach (var file in files) {
                OpenProjectAndPreserveLastPath(file);
            }

            UpdateMapSize();
        }
    }

    private void OpenProjectAsNew() {
        var files = GetFilesToOpen();

        if (files != null) {
            foreach (var file in files) {
                OpenProjectAsNew(file);
            }

            UpdateMapSize();
        }
    }

    private void SaveProject() {
        if (SelectedMapIndex != NoMapSelected) {
            var map = Maps[SelectedMapIndex];

            if (string.IsNullOrEmpty(map.LastPath)) {
                var file = GetPathToSave();

                if (file != null) {
                    SaveAndPreserveLastPath(file);
                }
            }
            else {
                SaveFromLastPath();
            }
        }
    }

    private void SaveProjectAs() {
        if (SelectedMapIndex != NoMapSelected) {
            var file = GetPathToSave();

            if (file != null) {
                SaveWithoutLastPath(file);
            }
        }
    }

    private void OpenProjectAndPreserveLastPath(string file) {
        var project = new Project();
        var map = project.Open(file);

        if (IsMapOpened(file)) {
            const string Message = "Este mapa já está aberto. Use a opção Open As New para abrir como uma nova cópia.";
            const string Title = "Abrir Mapa";

            MessageBox.Show(Message, Title);
        }
        else {
            if (map is not null) {
                map.LastPath = file;

                Maps.Add(map);

                SelectedMapIndex = Maps.Count - 1;

                TabMaps.TabPages.Add(map.Property.Name);
                TabMaps.SelectedIndex = SelectedMapIndex;
            }
        }
    }

    private void OpenProjectAsNew(string file) {
        var project = new Project();
        var map = project.Open(file);

        if (map is not null) {
            map.Property.Name += " Copy";

            Maps.Add(map);

            SelectedMapIndex = Maps.Count - 1;

            TabMaps.TabPages.Add(map.Property.Name);
            TabMaps.SelectedIndex = SelectedMapIndex;
        }
    }

    private void SaveAndPreserveLastPath(string file) {
        var project = new Project();

        var map = Maps[SelectedMapIndex];

        map.LastPath = file;

        project.Save(map, file);
    }

    private void SaveWithoutLastPath(string file) {
        var project = new Project();

        var map = Maps[SelectedMapIndex];

        project.Save(map, file);
    }

    private void SaveFromLastPath() {
        var project = new Project();
        var map = Maps[SelectedMapIndex];

        project.Save(map, map.LastPath);
    }

    private bool IsMapOpened(string path) {
        return Maps.FirstOrDefault(x => x.LastPath == path) != null;
    }

    #endregion

    #region Button Color 

    private void InitializeButton() {
        // Block
        ButtonForeColor0.BackColor = Color.FromArgb(60, Color.Red);
        // Npc Avoid
        ButtonForeColor1.BackColor = Color.FromArgb(60, Color.Gold);
        // Trap
        ButtonForeColor2.BackColor = Color.FromArgb(60, Color.Magenta);
        // Chat
        ButtonForeColor3.BackColor = Color.FromArgb(60, Color.Aquamarine);
        // Warp
        ButtonForeColor4.BackColor = Color.FromArgb(60, Color.SkyBlue);
    }

    private void ButtonForeColor_Click(object sender, EventArgs e) {
        var dialog = new ColorDialog() {
            AllowFullOpen = true,
            AnyColor = true,
            FullOpen = true,
            SolidColorOnly = false
        };

        SolidBrush brush;
        var result = dialog.ShowDialog();

        if (result == DialogResult.OK) {
            brush = new SolidBrush(dialog.Color);
            ((Button)sender).BackColor = dialog.Color;
        }
        else {
            return;
        }

        var full_name = ((Button)sender).Name;
        var index = full_name.Substring(full_name.Length - 1, 1);
        var name = full_name.Replace(index, string.Empty);

        if (name == "ButtonForeColor") {
            switch (int.Parse(index)) {
                case 0:
                    Colors.ForeBlock = brush;
                    break;
                case 1:
                    Colors.ForeAvoid = brush;
                    break;
                case 2:
                    Colors.ForeTrap = brush;
                    break;
                case 3:
                    Colors.ForeChat = brush;
                    break;
                case 4:
                    Colors.ForeWarp = brush;
                    break;
            }
        }

        PictureMap.Invalidate();
    }

    #endregion

    #region Menu File

    private void MenuFileNew_Click(object sender, EventArgs e) {
        AddMap();
    }

    private void MenuFileOpen_Click(object sender, EventArgs e) {
        OpenProject();
    }

    private void MenuFileOpenAs_Click(object sender, EventArgs e) {
        OpenProjectAsNew();
    }

    private void MenuFileSave_Click(object sender, EventArgs e) {
        SaveProject();
    }

    private void MenuFileSaveAs_Click(object sender, EventArgs e) {
        SaveProjectAs();
    }

    private void MenuFileSaveAll_Click(object sender, EventArgs e) {

    }

    private void MenuFileClose_Click(object sender, EventArgs e) {
        CloseProject();
    }

    private void MenuFileExit_Click(object sender, EventArgs e) {
        Application.Exit();
    }

    #endregion

    #region Menu Edit

    private void MenuEditFill_Click(object sender, EventArgs e) {
        if (SelectedMapIndex != NoMapSelected && Maps.Count > 0) {
            const string Message = "Você deseja preencher esta camada?";
            const string Title = "Preencher Camada";

            var result = MessageBox.Show(Message, Title, MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes) {
                FillLayer(GetTerrain());

                PictureMap.Invalidate();
            }
        }
    }

    private void MenuEditClear_Click(object sender, EventArgs e) {
        if (SelectedMapIndex != NoMapSelected && Maps.Count > 0) {
            const string Message = "Você deseja limpar esta camada?";
            const string Title = "Limpar Camada";

            var result = MessageBox.Show(Message, Title, MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes) {
                ClearLayer(GetTerrain());

                PictureMap.Invalidate();
            }
        }
    }

    #endregion

    #region Menu Export

    private void MenuExportEngine_Click(object sender, EventArgs e) {
        const int KeySize = 16;

        if (SelectedMapIndex != NoMapSelected) {
            var path = GetPathToExport("Engine", "Maps", "Exported Engine Maps");

            if (path != null) {
                var hash = Maps[SelectedMapIndex].Property.GetHash();
                var text = Maps[SelectedMapIndex].Property.GetHashText();

                Clipboard.SetText(text);

                var key = Hash.Compute(hash, KeySize, true);
                var iv = Hash.Compute(hash, KeySize, false);

                var export = new Export();
                export.ExportToEngine(Maps[SelectedMapIndex], key, iv, path);
            }
        }
    }

    private void MenuExportPng_Click(object sender, EventArgs e) {
        if (SelectedMapIndex != NoMapSelected) {
            var path = GetPathToExport("Png", "png", "Image Files");

            if (path != null) {
                var export = new Export();
                export.ExportToPng(Maps[SelectedMapIndex], path);
            }
        }
    }

    private string? GetPathToExport(string additionalpath, string extension, string description) {
        var dialog = new SaveFileDialog() {
            InitialDirectory = Application.StartupPath + "/Exported/" + additionalpath + @"/",
            Filter = $"{description} (*.{extension}) | *.{extension}",
            FilterIndex = 0
        };

        var result = dialog.ShowDialog();

        if (result == DialogResult.OK) {
            return dialog.FileName;
        }

        return null;
    }

    #endregion

    #region Menu Property

    private void MenuProperty_Click(object sender, EventArgs e) {
        if (Maps.Count > 0) {
            if (SelectedMapIndex != NoMapSelected) {
                var property = Maps[SelectedMapIndex].Property;
                new FormProperty(JetBrainsMono, property, this).ShowDialog();
            }
        }
    }

    #endregion

    #region Menu View

    private void MenuViewItem_Click(object sender, EventArgs e) {
        PictureMap.Invalidate();
    }

    #endregion

    #region Tabs

    private void TabMaps_SelectedIndexChanged(object sender, EventArgs e) {
        var old = SelectedMapIndex;

        SelectedMapIndex = TabMaps.SelectedIndex;

        if (SelectedMapIndex != old) {
            if (SelectedMapIndex != NoMapSelected) {
                UpdateMapSize();
            }
        }
    }

    #endregion

    #region Grid

    private void ScrollGridOpacity_Scroll(object sender, ScrollEventArgs e) {
        LabelGridOpacity.Text = "Grid Map: " + ScrollGridOpacity.Value;

        Grid.Pen = new Pen(Color.FromArgb(ScrollGridOpacity.Value, 255, 255, 255));
        Grid.Update();

        PictureMap.Invalidate();
    }

    #endregion

    #region Direction

    private bool IsDirBlocked(uint blockVar, int dir) {
        var dirResult = Convert.ToUInt32(Math.Pow(2, dir));
        var result = (~blockVar & dirResult);

        if (Convert.ToBoolean(result)) {
            return false;
        }

        return true;
    }

    private void SetDirBlocked(ref uint blockVar, int dir, bool block) {
        var dirResult = Convert.ToUInt32(Math.Pow(2, dir));
        uint value;

        if (block) {
            value = blockVar | dirResult;
        }
        else {
            value = blockVar & ~dirResult;
        }

        blockVar = value;
    }

    #endregion

    #region Group Layer - Area Radio 

    private void RadioLayer_CheckedChanged(object sender, EventArgs e) {
        var r = ((Control)sender).Name == "RadioAttributes";

        GroupAttributes.Enabled = r;

        ButtonClearDirection.Enabled = ((Control)sender).Name == "RadioDirection";

        PictureMap.Invalidate();
    }

    private void ButtonClearDirection_Click(object sender, EventArgs e) {
        ClearDirections();
    }

    private void ClearDirections() {
        if (SelectedMapIndex != NoMapSelected && Maps.Count > 0) {
            var map = Maps[SelectedMapIndex];
            var property = map.Property;

            var width = property.Width;
            var height = property.Height;

            for (var x = 0; x < width; ++x) {
                for (var y = 0; y < height; ++y) {
                    var attribute = map.Attribute[x, y];

                    attribute.DirBlock = 0;

                    map.Attribute[x, y] = attribute;
                }
            }

            PictureMap.Invalidate();
        }
    }

    #endregion

    #region Tileset

    private void UpdateTilesets() {
        var id = 1;
        var count = Tiles.Count;

        SelectedTileset = id.ToString();
        LabelTileId.Text = "Tile Id: " + SelectedTileset;

        ScrollTileId.Maximum = count;
        ScrollTileId.Minimum = 1;
        ScrollTileId.Value = id;
    }

    private void UpdateTileScrollBars() {
        if (!string.IsNullOrEmpty(SelectedTileset)) {
            var tile = Tiles.Get(SelectedTileset);

            if (tile != null) {
                var width = tile.Width;
                var height = tile.Height;

                var x = Convert.ToInt32(Math.Abs(PictureTile.Width - width) / Constants.TileSize);

                ScrollTileX.Maximum = x;
                ScrollTileX.Minimum = 0;
                ScrollTileX.Value = 0;

                var y = Convert.ToInt32(Math.Abs(PictureTile.Height - height) / Constants.TileSize);

                ScrollTileY.Maximum = y;
                ScrollTileY.Minimum = 0;
                ScrollTileY.Value = 0;

                PictureTile.Invalidate();
            }
        }
    }

    private void ScrollTileId_Scroll(object sender, ScrollEventArgs e) {
        SelectedTileset = ScrollTileId.Value.ToString();
        LabelTileId.Text = "Tile Id: " + SelectedTileset;

        UpdateTileScrollBars();

        PictureTile.Invalidate();
    }

    private void ScrollTileY_Scroll(object sender, ScrollEventArgs e) {
        PictureTile.Invalidate();
    }

    private void ScrollTileX_Scroll(object sender, ScrollEventArgs e) {
        PictureTile.Invalidate();
    }

    private void PictureTile_Paint(object sender, PaintEventArgs e) {
        var g = e.Graphics;

        g.Clear(Color.Black);

        if (!string.IsNullOrEmpty(SelectedTileset)) {
            var block = Constants.TileSize;
            var x = ScrollTileX.Value;
            var y = ScrollTileY.Value;

            var tile = Tiles.Get(SelectedTileset);

            if (tile != null) {
                g.DrawImage(tile, new Rectangle(-x * block, -y * block, tile.Width, tile.Height));
            }
        }

        g.DrawRectangle(Pens.Red, SelectedTileRectangle);
    }

    private void PictureTile_MouseMove(object sender, MouseEventArgs e) {
        if (IsTileClickPressed) {
            var startX = SelectedTileRectangle.X;
            var startY = SelectedTileRectangle.Y;

            var blockSize = Constants.TileSize;

            var eX = (e.X < 0) ? 0 : e.X;
            var eY = (e.Y < 0) ? 0 : e.Y;

            eX = (eX > PictureTile.Width) ? PictureTile.Width : eX;
            eY = (eY > PictureTile.Height) ? PictureTile.Height : eY;

            var x = Convert.ToInt32(eX / blockSize) * blockSize;
            var y = Convert.ToInt32(eY / blockSize) * blockSize;

            x -= startX;
            y -= startY;

            if (x < 0) {
                x = 0;
            }

            if (y < 0) {
                y = 0;
            }

            SelectedTileRectangle = new Rectangle(startX, startY, x + blockSize, y + blockSize);

            PictureTile.Invalidate();
        }
    }

    private void PictureTile_MouseDown(object sender, MouseEventArgs e) {
        var blockSize = Constants.TileSize;

        var eX = (e.X < 0) ? 0 : e.X;
        var eY = (e.Y < 0) ? 0 : e.Y;

        eX = (eX > PictureTile.Width) ? PictureTile.Width : eX;
        eY = (eY > PictureTile.Height) ? PictureTile.Height : eY;

        var x = (Convert.ToInt32(eX / blockSize) * blockSize);
        var y = (Convert.ToInt32(eY / blockSize) * blockSize);

        SelectedTileRectangle = new Rectangle(x, y, blockSize, blockSize);

        PictureTile.Invalidate();

        IsTileClickPressed = true;
    }

    private void PictureTile_MouseUp(object sender, MouseEventArgs e) {
        IsTileClickPressed = false;
    }

    #endregion

    #region Form

    private void FormMain_Resize(object sender, EventArgs e) {
        UpdateTileScrollBars();
        UpdateMapScrollBars();
    }

    private void FormMain_Load(object sender, EventArgs e) {
        var size = Constants.TileSize;

        SelectedTileRectangle = new Rectangle(0, 0, size, size);

        ChangeFont(Controls[0]);

        UpdateTilesets();
        UpdateTileScrollBars();

        PictureMap.Invalidate();
    }

    #endregion

    #region Map

    private void AddMap() {
        var map = new Map();

        Maps.Add(map);

        SelectedMapIndex = Maps.Count - 1;

        TabMaps.TabPages.Add(map.Property.Name);

        var property = Maps[SelectedMapIndex].Property;

        new FormProperty(JetBrainsMono, property, this).ShowDialog();
    }

    public void UpdateMapSize() {
        if (SelectedMapIndex != NoMapSelected) {
            var map = Maps[SelectedMapIndex];

            map.UpdateSize();

            var width = map.Property.Width;
            var height = map.Property.Height;

            Grid.Update(width, height);
            UpdateMapScrollBars();

            PictureMap.Invalidate();
        }
    }

    public void ChangeSelectedMapName(string name) {
        if (Maps.Count > 0) {
            if (SelectedMapIndex != NoMapSelected) {
                TabMaps.TabPages[SelectedMapIndex].Text = name;
            }
        }
    }

    private void UpdateMapScrollBars() {
        if (SelectedMapIndex != NoMapSelected) {
            var map = Maps[SelectedMapIndex];

            var block = Constants.TileSize;

            var width = map.Property.Width * block;
            var height = map.Property.Height * block;

            var x = Convert.ToInt32(Math.Abs(PictureMap.Width - width) / block);

            ScrollMapX.Maximum = x;
            ScrollMapX.Minimum = 0;
            ScrollMapX.Value = 0;

            var y = Convert.ToInt32(Math.Abs(PictureMap.Height - height) / block);

            ScrollMapY.Maximum = y;
            ScrollMapY.Minimum = 0;
            ScrollMapY.Value = 0;

            LabelMapX.Text = "Scroll X: " + ScrollMapX.Value;
            LabelMapY.Text = "Scroll Y: " + ScrollMapY.Value;

            PictureMap.Invalidate();
        }
    }

    private void PictureMap_Paint(object sender, PaintEventArgs e) {
        var g = e.Graphics;

        var startx = ScrollMapX.Value;
        var starty = ScrollMapY.Value;

        var block = Constants.TileSize;

        g.Clear(Color.Black);

        if (SelectedMapIndex != NoMapSelected) {
            var map = Maps[SelectedMapIndex];

            var width = map.Property.Width;
            var height = map.Property.Height;

            if (MenuViewGround.Checked) {
                DrawTerrain(g, startx, starty, width, height, map.Ground);
            }

            if (MenuViewMask1.Checked) {
                DrawTerrain(g, startx, starty, width, height, map.Mask1);
            }

            if (MenuViewMask2.Checked) {
                DrawTerrain(g, startx, starty, width, height, map.Mask2);
            }

            if (MenuViewFringe1.Checked) {
                DrawTerrain(g, startx, starty, width, height, map.Fringe1);
            }

            if (MenuViewFringe2.Checked) {
                DrawTerrain(g, startx, starty, width, height, map.Fringe2);
            }

            if (RadioAttributes.Checked) {
                DrawAttributes(g, startx, starty, width, height, map.Attribute);
            }
            else if (RadioDirection.Checked) {
                DrawDirection(g, startx, starty);
            }

            if (MenuViewGrid.Checked) {
                g.DrawImage(Grid.GetGrid(), new Rectangle(0, 0, PictureMap.Width, PictureMap.Height), new Rectangle(startx * block, starty * block, PictureMap.Width, PictureMap.Height), GraphicsUnit.Pixel);
                g.DrawRectangle(Grid.Pen, new Rectangle(0, 0, Grid.GetGrid().Width, Grid.GetGrid().Height));
            }
        }
    }

    private void DrawTerrain(Graphics g, int startx, int starty, int width, int height, ImageBlock[,] terrain) {
        var size = Constants.TileSize;
        var source = new Rectangle(0, 0, size, size);

        var limitx = (PictureMap.Width / size) + 1;
        var limity = (PictureMap.Height / size) + 1;

        if (limitx > width) {
            limitx = width;
        }

        if (limity > height) {
            limity = height;
        }

        for (var x = 0; x < limitx; ++x) {
            for (var y = 0; y < limity; ++y) {
                var destx = startx + x;
                var desty = starty + y;

                var dest = new Rectangle(x * size, y * size, size, size);

                if (destx < width && desty < height) {
                    g.DrawImage(terrain[destx, desty].Texture, dest, source, GraphicsUnit.Pixel);
                }
            }
        }
    }

    private void PictureMap_MouseDown(object sender, MouseEventArgs e) {
        if (SelectedMapIndex != NoMapSelected) {
            PictureMap_MouseDown(e);

            IsMapClickPressed = true;
        }
    }

    private void PictureMap_MouseUp(object sender, MouseEventArgs e) {
        IsMapClickPressed = false;
    }

    private void PictureMap_MouseMove(object sender, MouseEventArgs e) {
        if (SelectedMapIndex != NoMapSelected && IsMapClickPressed) {
            PictureMap_MouseDown(e);
        }
    }

    private void PictureMap_MouseDown(MouseEventArgs e) {
        var tileSize = Constants.TileSize;

        var startx = ScrollMapX.Value;
        var starty = ScrollMapY.Value;

        // Não permite que ultrapasse os limites.
        var eX = (e.X < 0) ? 0 : e.X;
        var eY = (e.Y < 0) ? 0 : e.Y;

        // Não permite que ultrapasse os limites.
        eX = (eX > PictureMap.Width) ? PictureMap.Width : eX;
        eY = (eY > PictureMap.Height) ? PictureMap.Height : eY;

        var x = Convert.ToInt32(eX / tileSize);
        var y = Convert.ToInt32(eY / tileSize);

        if (RadioAttributes.Checked) {
            if (e.Button == MouseButtons.Left) {
                SelectAttribute(startx + x, starty + y);
            }
            else if (e.Button == MouseButtons.Right) {
                DiselectAttribute(startx + x, starty + y);
            }
        }
        else if (RadioDirection.Checked) {
            if (e.Button == MouseButtons.Left) {
                SetDirection(startx + x, starty + y, eX, eY);
            }
        }
        else {
            if (e.Button == MouseButtons.Right) {
                ClearTexture(GetTerrain(), startx + x, starty + y);
            }
            else if (e.Button == MouseButtons.Left) {
                ApplyTexture(GetTerrain(), startx, starty, x, y, tileSize);
            }
        }

        PictureMap.Invalidate();
    }

    private Terrain GetTerrain() {
        if (RadioGround.Checked) {
            return Terrain.Ground;
        }
        else if (RadioMask1.Checked) {
            return Terrain.Mask1;
        }
        else if (RadioMask2.Checked) {
            return Terrain.Mask2;
        }
        else if (RadioFringe1.Checked) {
            return Terrain.Fringe1;
        }
        else if (RadioFringe2.Checked) {
            return Terrain.Fringe2;
        }

        return Terrain.Ground;
    }

    private void ScrollMapX_Scroll(object sender, ScrollEventArgs e) {
        LabelMapX.Text = "Scroll X: " + ScrollMapX.Value;
        PictureMap.Invalidate();
    }

    private void ScrollMapY_Scroll(object sender, ScrollEventArgs e) {
        LabelMapY.Text = "Scroll Y: " + ScrollMapY.Value;
        PictureMap.Invalidate();
    }

    private void ApplyTexture(Terrain terrainType, int startX, int startY, int x, int y, int size) {
        var texture = Tiles.Get(SelectedTileset);

        if (texture is not null) {
            var sourceX = SelectedTileRectangle.X + (ScrollTileX.Value * size);
            var sourceY = SelectedTileRectangle.Y + (ScrollTileY.Value * size);

            var sourceWidth = SelectedTileRectangle.Width;
            var sourceHeight = SelectedTileRectangle.Height;

            var map = Maps[SelectedMapIndex];

            // Se houver somente 1 bloco selecionado.
            // Aplica a textura no terreno.
            if (sourceWidth == size && sourceHeight == size) {
                map.Apply(terrainType, startX + x, startY + y, sourceX, sourceY, texture);
            }
            // Se houver vários blocos selecionados. 
            else if (sourceWidth > size || sourceHeight > 0) {
                var xCount = sourceWidth / size;
                var yCount = sourceHeight / size;

                // Aplica a textura continuamente.
                for (var _x = 0; _x < xCount; ++_x) {
                    for (var _y = 0; _y < yCount; ++_y) {

                        var destx = startX + x + _x;
                        var desty = startY + y + _y;

                        map.Apply(terrainType, destx, desty, sourceX + (_x * size), sourceY + (_y * size), texture);
                    }
                }
            }
        }
    }

    private void ClearTexture(Terrain terrainType, int x, int y) {
        if (SelectedMapIndex != NoMapSelected) {
            var map = Maps[SelectedMapIndex];
            map.Clear(terrainType, x, y);
        }
    }

    private void ClearLayer(Terrain terrainType) {
        if (SelectedMapIndex != NoMapSelected && Maps.Count > 0) {
            var property = Maps[SelectedMapIndex].Property;

            var width = property.Width;
            var height = property.Height;

            for (var x = 0; x < width; ++x) {
                for (var y = 0; y < height; ++y) {
                    ClearTexture(terrainType, x, y);
                }
            }
        }
    }

    private void FillLayer(Terrain terrainType) {
        if (SelectedMapIndex != NoMapSelected && Maps.Count > 0) {
            var property = Maps[SelectedMapIndex].Property;

            var width = property.Width;
            var height = property.Height;

            for (var x = 0; x < width; ++x) {
                for (var y = 0; y < height; ++y) {
                    ApplyTexture(terrainType, 0, 0, x, y, Constants.TileSize);
                }
            }
        }
    }

    #endregion

    #region Attribute 

    private void SelectAttribute(int x, int y) {
        SetAttributeValue(x, y, true);
    }

    private void DiselectAttribute(int x, int y) {
        SetAttributeValue(x, y, false);
    }

    private void SetAttributeValue(int x, int y, bool add) {
        if (SelectedMapIndex != NoMapSelected && Maps.Count > 0) {
            var map = Maps[SelectedMapIndex];
            var width = map.Property.Width;
            var height = map.Property.Height;

            if (x >= width || y >= height) {
                return;
            }

            var tileType = TileType.Walkable;

            if (RadioBlock.Checked) {
                tileType = TileType.Blocked;
            }
            else if (RadioAvoid.Checked) {
                tileType = TileType.NpcAvoid;
            }
            else if (RadioTrap.Checked) {
                tileType = TileType.Trap;
            }
            else if (RadioChat.Checked) {
                tileType = TileType.Chat;
            }
            else if (RadioWarp.Checked) {
                tileType = TileType.Warp;
            }

            var attribute = map.Attribute[x, y];

            if (add) {
                attribute.Type = tileType;
            }
            else {
                attribute.Type = 0;
                attribute.Data1 = 0;
                attribute.Data2 = 0;
                attribute.Data3 = 0;
                attribute.Data4 = 0;
                attribute.Data5 = 0;
            }

            map.Attribute[x, y] = attribute;
        }
    }

    private void ClearAttributes() {
        if (SelectedMapIndex != NoMapSelected && Maps.Count > 0) {
            var map = Maps[SelectedMapIndex];
            var property = map.Property;

            var width = property.Width;
            var height = property.Height;

            for (var x = 0; x < width; ++x) {
                for (var y = 0; y < height; ++y) {
                    map.Attribute[x, y] = new Tile();
                }
            }

            PictureMap.Invalidate();
        }
    }

    private void DrawAttributes(Graphics g, int startx, int starty, int width, int height, Tile[,] attributes) {
        var size = Constants.TileSize;

        var limitx = (PictureMap.Width / size) + 1;
        var limity = (PictureMap.Height / size) + 1;

        if (limitx > width) {
            limitx = width;
        }

        if (limity > height) {
            limity = height;
        }

        for (var x = 0; x < limitx; ++x) {
            for (var y = 0; y < limity; ++y) {
                var destx = startx + x;
                var desty = starty + y;

                if (destx < width && desty < height) {
                    DrawAttribute(g, attributes[destx, desty], x * size, y * size);
                }
            }
        }
    }

    private void DrawAttribute(Graphics g, Tile attributes, int x, int y) {
        var letter = string.Empty;
        var back = Brushes.White;
        var fore = Brushes.White;

        switch (attributes.Type) {
            case TileType.Blocked:
                letter = "B";
                back = Colors.BackBlock;
                fore = Colors.ForeBlock;

                break;
            case TileType.Warp:
                letter = "W";
                back = Colors.BackWarp;
                fore = Colors.ForeWarp;

                break;
            case TileType.Trap:
                letter = "T";
                back = Colors.BackTrap;
                fore = Colors.ForeTrap;

                break;
            case TileType.NpcAvoid:
                letter = "A";
                back = Colors.BackAvoid;
                fore = Colors.ForeAvoid;

                break;
            case TileType.Chat:
                letter = "C";
                back = Colors.BackChat;
                fore = Colors.ForeChat;
                break;
        }

        var size = Constants.TileSize;

        if (attributes.Type != TileType.Walkable) {
            g.FillRectangle(back, new RectangleF(x, y, size, size));
            g.DrawString(letter, JetBrainsMono.GetFont(FontStyle.Regular), fore, x + 9, y + 8);
        }
    }

    private void ButtonClearAttribute_Click(object sender, EventArgs e) {
        ClearAttributes();
    }

    #endregion

    #region Direction

    private void DrawDirection(Graphics g, int startx, int starty) {
        var destination = new Rectangle() {
            Width = DirectionImageSize,
            Height = DirectionImageSize
        };

        var source = new Rectangle {
            Width = DirectionImageSize,
            Height = DirectionImageSize
        };

        var size = Constants.TileSize;

        var map = Maps[SelectedMapIndex];

        var width = map.Property.Width;
        var height = map.Property.Height;

        var limitx = (PictureMap.Width / size) + 1;
        var limity = (PictureMap.Height / size) + 1;

        if (limitx > width) {
            limitx = width;
        }

        if (limity > height) {
            limity = height;
        }

        for (var x = 0; x < limitx; ++x) {
            for (var y = 0; y < limity; ++y) {
                var destx = startx + x;
                var desty = starty + y;

                if (destx < width && desty < height) {

                    var attribute = map.Attribute[destx, desty];

                    for (var i = 0; i < Constants.MaximumDirection; i++) {
                        destination.X = x * size + DirectionPosition[i].X;
                        destination.Y = y * size + DirectionPosition[i].Y;

                        source.X = i * DirectionImageSize;

                        if (IsDirBlocked(attribute.DirBlock, i)) {
                            source.Y = DirectionImageSize * 2;
                        }
                        else {
                            source.Y = DirectionImageSize;
                        }

                        g.DrawImage(TextureDirection, destination, source, GraphicsUnit.Pixel);
                    }
                }
            }
        }
    }

    private void SetDirection(int clicked_x, int clicked_y, int mouseX, int mouseY) {
        var map = Maps[SelectedMapIndex];

        var width = map.Property.Width;
        var height = map.Property.Height;

        if (clicked_x >= width || clicked_y >= height) {
            return;
        }

        var tileSize = Constants.TileSize;

        var startx = ScrollMapX.Value;
        var starty = ScrollMapY.Value;

        var x = (mouseX + startx * tileSize) - (clicked_x * tileSize);
        var y = (mouseY + starty * tileSize) - (clicked_y * tileSize);

        for (var i = 0; i < Constants.MaximumDirection; ++i) {
            if (x >= DirectionPosition[i].X && x <= DirectionPosition[i].X + DirectionImageSize) {
                if (y >= DirectionPosition[i].Y && y <= DirectionPosition[i].Y + DirectionImageSize) {

                    var attribute = map.Attribute[clicked_x, clicked_y];

                    var value = Convert.ToUInt32(attribute.DirBlock);

                    SetDirBlocked(ref value, i, !IsDirBlocked(value, i));

                    attribute.DirBlock = (byte)value;

                    map.Attribute[clicked_x, clicked_y] = attribute;
                }
            }
        }
    }

    #endregion
}