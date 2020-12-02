using System;
using System.Threading.Tasks;
using FFImageLoading;
using FFImageLoading.Svg.Platform;
using NotesManager.custom;
using NotesManager.iOS.custom.renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabPage), typeof(PageTabRenderer))]

namespace NotesManager.iOS.custom.renderers
{
    [Foundation.Preserve(AllMembers = true)]
    public class PageTabRenderer : TabbedRenderer
    {
        readonly nfloat imageYOffset = 7.0f;


        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (TabBar.Items != null)
            {
                foreach (var item in TabBar.Items)
                {
                    item.Title = null;
                    item.ImageInsets = new UIEdgeInsets(imageYOffset, 0, -imageYOffset, 0);
                }
            }
        }


        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            //accessing xamarin global resources
            TabBar.BackgroundColor = ((Color) App.GetResourceValue("TabsColor")).ToUIColor();
        }

        protected override async Task<Tuple<UIImage, UIImage>> GetIcon(Page page)
        {
            var navigationPage = page as NavigationPage;
            if (navigationPage != null && navigationPage.CurrentPage != null)
            {
                var imageSource = navigationPage.IconImageSource == null
                    ? navigationPage.CurrentPage.IconImageSource
                    : navigationPage.IconImageSource;
                return await this.GetNativeUIImage(imageSource);
            }

            return await this.GetNativeUIImage(page.IconImageSource);
        }

        private async Task<Tuple<UIImage, UIImage>> GetNativeUIImage(ImageSource imageSource)
        {
            var imageicon = await GetNativeImageAsync(imageSource);
            return new Tuple<UIImage, UIImage>(imageicon, null);
        }

        private async Task<UIImage> GetNativeImageAsync(ImageSource imageSource)
        {
            if (imageSource is FileImageSource fileImage && fileImage.File.Contains(".svg"))
            {
                var imageicon = await ImageService.Instance.LoadFile(fileImage.File)
                    .WithCustomDataResolver(new SvgDataResolver()).AsUIImageAsync();
                return imageicon.ImageWithRenderingMode(UIImageRenderingMode.Automatic);
            }

            else
            {
                var imageicon = await GetUIImage(imageSource);
                return imageicon.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            }
        }

        private Task<UIImage> GetUIImage(ImageSource imageSource)
        {
            var handler = GetImageSourceHandler(imageSource);
            return handler.LoadImageAsync(imageSource);
        }

        private static IImageSourceHandler GetImageSourceHandler(ImageSource source)
        {
            IImageSourceHandler sourceHandler = null;
            if (source is UriImageSource)
                sourceHandler = new ImageLoaderSourceHandler();
            else if (source is FileImageSource)
                sourceHandler = new FileImageSourceHandler();
            else if (source is StreamImageSource)
                sourceHandler = new StreamImagesourceHandler();
            else if (source is FontImageSource)
                sourceHandler = new FontImageSourceHandler();

            return sourceHandler;
        }
    }
}