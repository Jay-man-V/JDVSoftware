////-----------------------------------------------------------------------
//// <copyright file="ImageToMediaImageConverterTests.cs" company="JDV Software Ltd">
////     Copyright (c) JDV Software Ltd. All rights reserved.
//// </copyright>
////-----------------------------------------------------------------------

//using System.Drawing;
//using System.Globalization;
//using System.Runtime.InteropServices;
//using System.Windows;
//using System.Windows.Data;
//using System.Windows.Interop;
//using System.Windows.Media.Imaging;

//using NUnit.Framework;

//using Foundation.Tests.Unit.Mocks;
//using Foundation.Tests.Unit.Support;

//using Foundation.Common;

//using FCONV = Foundation.Common;

//namespace Foundation.Tests.Unit.Foundation.Views.ConvertersTests
//{
//    /// <summary>
//    /// The unit test ImageToMediaImageConverterTests class
//    /// </summary>
//    [TestFixture]
//    public class ImageToMediaImageConverterTests : UnitTestBase
//    {
//        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
//        [return: MarshalAs(UnmanagedType.Bool)]
//        public static extern Boolean DeleteObject([In] IntPtr hObject);

//        private IValueConverter GetConverter()
//        {
//            return new FCONV.ImageToMediaImageConverter();
//        }

//        private void CompareBitmaps(BitmapSource bitmap1, BitmapSource bitmap2)
//        {
//            Byte[] array1 = bitmap1.ToByteArray();
//            Byte[] array2 = bitmap2.ToByteArray();

//            Assert.That(array2, Is.EquivalentTo(array1));
//        }

//        private void CompareBitmaps(Bitmap bitmap1, Bitmap bitmap2)
//        {
//            Byte[] array1 = bitmap1.ToByteArray();
//            Byte[] array2 = bitmap2.ToByteArray();

//            Assert.That(array2, Is.EquivalentTo(array1));
//        }

//        /// <summary>
//        /// Tests the Convert method.
//        /// </summary>
//        [TestCase]
//        public void TestConvert_Null()
//        {
//            IValueConverter converter = GetConverter();
//            Type targetType = typeof(BitmapSource);
//            Object parameter = null;
//            CultureInfo culture = null;

//            Object value = null;
//            Object expectedValue = null;

//            Object actualResult = converter.Convert(value, targetType, parameter, culture);

//            CompareBitmaps((BitmapSource)expectedValue, (BitmapSource)actualResult);
//        }

//        /// <summary>
//        /// Tests the Convert method.
//        /// </summary>
//        [TestCase]
//        public void TestConvert_NonBitmapSource()
//        {
//            IValueConverter converter = GetConverter();
//            Type targetType = typeof(BitmapSource);
//            Object parameter = null;
//            CultureInfo culture = null;

//            Object value = new RandomObject();
//            Object expectedValue = null;

//            Object actualResult = converter.Convert(value, targetType, parameter, culture);

//            CompareBitmaps((BitmapSource)expectedValue, (BitmapSource)actualResult);
//        }

//        /// <summary>
//        /// Tests the Convert method.
//        /// </summary>
//        [TestCase]
//        [DeploymentItem(@".Support\SampleDocuments\32BitColour_16x16.bmp", @".Support\SampleDocuments\")]
//        public void TestConvert_Valid()
//        {
//            IValueConverter converter = GetConverter();
//            Type targetType = typeof(BitmapSource);
//            Object parameter = null;
//            CultureInfo culture = null;

//            Bitmap value = Image.FromFile(@".Support\SampleDocuments\32BitColour_16x16.bmp") as Bitmap;

//            IntPtr handle = value.GetHbitmap();
//            IntPtr pallette = IntPtr.Zero;
//            Int32Rect sourceRect = Int32Rect.Empty;
//            BitmapSizeOptions sizeOptions = BitmapSizeOptions.FromEmptyOptions();

//            Object expectedValue = Imaging.CreateBitmapSourceFromHBitmap(handle, pallette, sourceRect, sizeOptions);

//            DeleteObject(handle);

//            Object actualResult = converter.Convert(value, targetType, parameter, culture);

//            CompareBitmaps((BitmapSource)expectedValue, (BitmapSource)actualResult);
//        }

//        /// <summary>
//        /// Tests the Convert method.
//        /// </summary>
//        [TestCase]
//        public void TestConvertBack_Null()
//        {
//            IValueConverter converter = GetConverter();
//            Type targetType = typeof(Bitmap);
//            Object parameter = null;
//            CultureInfo culture = null;

//            Object value = null;
//            //Object expectedValue = null;

//            Object actualResult = converter.ConvertBack(value, targetType, parameter, culture);

//            Assert.That(actualResult, Is.EqualTo(null));
//        }

//        /// <summary>
//        /// Tests the Convert method.
//        /// </summary>
//        [TestCase]
//        public void TestConvertBack_NonBitmap()
//        {
//            IValueConverter converter = GetConverter();
//            Type targetType = typeof(Bitmap);
//            Object parameter = null;
//            CultureInfo culture = null;

//            RandomObject value = new RandomObject();
//            //Object expectedValue = null;

//            Object actualResult = converter.ConvertBack(value, targetType, parameter, culture);

//            Assert.That(actualResult, Is.EqualTo(null));
//        }

//        /// <summary>
//        /// Tests the Convert method.
//        /// </summary>
//        [TestCase]
//        [DeploymentItem(@".Support\SampleDocuments\32BitColour_16x16.bmp", @".Support\SampleDocuments\")]
//        public void TestConvertBack_Valid()
//        {
//            IValueConverter converter = GetConverter();
//            Type targetType = typeof(Bitmap);
//            Object parameter = null;
//            CultureInfo culture = null;

//            Bitmap input = Image.FromFile(@".Support\SampleDocuments\32BitColour_16x16.bmp") as Bitmap;

//            IntPtr handle = input.GetHbitmap();
//            IntPtr pallette = IntPtr.Zero;
//            Int32Rect sourceRect = Int32Rect.Empty;
//            BitmapSizeOptions sizeOptions = BitmapSizeOptions.FromEmptyOptions();

//            BitmapSource value = Imaging.CreateBitmapSourceFromHBitmap(handle, pallette, sourceRect, sizeOptions);
//            DeleteObject(handle);

//            Object expectedValue = Image.FromFile(@".Support\SampleDocuments\32BitColour_16x16.bmp") as Bitmap;

//            Object actualResult = converter.ConvertBack(value, targetType, parameter, culture);

//            CompareBitmaps((Bitmap)expectedValue, (Bitmap)actualResult);
//        }
//    }
//}
