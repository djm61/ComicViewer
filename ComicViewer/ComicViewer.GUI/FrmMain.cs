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

        public FrmMain()
        {
            InitializeComponent();
            pbMain.SizeMode = PictureBoxSizeMode.AutoSize;
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
                    }
                    catch (Exception ex)
                    {
                        var message = $"{_comic.ErrorMessage}\r\n{ex.Message}";
                        MessageBox.Show(message);
                    }
                }
            }
        }
    }
}
