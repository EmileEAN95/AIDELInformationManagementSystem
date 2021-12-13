using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using AIDELInformationManagementSystem.Properties;
using System.Resources;

namespace ColorPicker
{
    class ImagesUtil
    {
        static public ImageList GetToolbarImageList(Type type, Image _targetImage, Size imageSize, Color transparentColor)
        {
            System.Drawing.Bitmap bitmap = new Bitmap(_targetImage);
            ImageList imageList = new ImageList();
            imageList.ImageSize = imageSize;
            imageList.TransparentColor = transparentColor;
            imageList.Images.AddStrip(bitmap);
            imageList.ColorDepth = ColorDepth.Depth24Bit;
            return imageList;
        }
    }

    class SelectorImages
    {
        public enum eIndexes
        {
            Right,
            Left,
            Up,
            Down,
            Donut,
        }

        static private ImageList m_imageList = null;
        static public ImageList ImageList()
        {
            Type t = typeof(SelectorImages);
            if (m_imageList == null)
                m_imageList = ImagesUtil.GetToolbarImageList(t, Resources.colorbarIndicators, new Size(12, 12), Color.Magenta);
            return m_imageList;
        }
        static public Image Image(eIndexes index)
        {
            return ImageList().Images[(int)index];
        }
    }
    class PopupContainerImages
    {
        public enum eIndexes
        {
            Close,
            Check,
        }

        static private ImageList m_imageList = null;
        static public ImageList ImageList()
        {
            Type t = typeof(SelectorImages);
            if (m_imageList == null)
                m_imageList = ImagesUtil.GetToolbarImageList(t, Resources.popupcontainerbuttons, new Size(16, 16), Color.Magenta);
            return m_imageList;
        }
        static public Image Image(eIndexes index)
        {
            return ImageList().Images[(int)index];
        }
    }
}
