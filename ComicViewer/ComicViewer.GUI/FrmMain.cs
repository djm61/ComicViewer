using System;
using System.Drawing;
using System.Windows.Forms;
using ComicViewer.Base;
using ComicViewer.Base.Comic;
using ComicViewer.Base.Interfaces;

namespace ComicViewer.GUI
{
    public partial class FrmMain : Form
    {
        private IComicFactory<Comic> _comicFactory;
        private Comic _comic;
        private int _page;
        private int _imageWidth;
        private int _imageHeight;
        private float _imageScale = 1.0f;

        public FrmMain()
        {
            InitializeComponent();
            pbMain.SizeMode = PictureBoxSizeMode.AutoSize;
            lblScale.Text = string.Empty;

            _page = 0;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.CheckFileExists = true;
                dialog.Multiselect = false;
                dialog.DefaultExt = "*.cbr|*.cbz";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var path = dialog.FileName;
                        _comicFactory = new ComicFactory(path);
                        _comic = _comicFactory.Build();
                        _comic.GenerateCover();
                        var bitmap = new Bitmap(_comic.CoverPath);
                        pbMain.Image = bitmap;
                        pbMain.SizeMode = PictureBoxSizeMode.AutoSize;
                        _imageWidth = bitmap.Width;
                        _imageHeight = bitmap.Height;
                        _page = 0;
                        lblScale.Text = _imageScale.ToString("p0");
                        pbMain.MouseWheel += PbMain_MouseWheel;
                    }
                    catch (Exception ex)
                    {
                        var message = $"{_comic.ErrorMessage}\r\n{ex.Message}";
                        MessageBox.Show(message);
                    }
                }
            }
        }

        private void PbMain_MouseWheel(object sender, MouseEventArgs e)
        {
            // The amount by which we adjust scale per wheel click.
            const float scalePerDelta = 0.1f / 120;

            // Update the drawing based upon the mouse wheel scrolling.
            _imageScale += e.Delta * scalePerDelta;
            if (_imageScale < 0)
            {
                _imageScale = 0;
            }

            // Size the image.
            var width = (int) (_imageWidth * _imageScale);
            var height = (int) (_imageHeight * _imageScale);
            pbMain.Size = new Size(width, height);

            // Display the new scale.
            lblScale.Text = _imageScale.ToString("p0");
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            var pb = sender as PictureBox;
            if (pb == null)
            {
                return;
            }

            var coordinates = e.Location;

            var width = pb.Width;
            var halfWidth = width / 2;
            if (coordinates.X >= halfWidth)
            {
                //next image
                _page++;
            }
            else if (coordinates.X < halfWidth)
            {
                //previous image
                _page--;
            }
            else
            {
                MessageBox.Show(
                    $@"coordinates.X[{coordinates.X}] is not less-than or equal to halfWidth[{halfWidth}] AND not less-than halfWidth[{halfWidth}]");
                return;
            }

            _comic.GetPage(_page);
        }
    }
}
