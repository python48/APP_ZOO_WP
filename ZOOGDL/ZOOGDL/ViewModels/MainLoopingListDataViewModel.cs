
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace ZOOGDL.ViewModels
{
    public class MainLoopingListDataViewModel
    {
        private LoopingListDataSource loopingListDataSource;

        /// <summary>
        /// Initializes the looping list data source that will be used in RadLoopingList.
        /// </summary>
        private void InitializeLoopingListDataSource()
        {
            this.loopingListDataSource = new LoopingListDataSource(10);
            this.loopingListDataSource.ItemNeeded += new EventHandler<LoopingListDataItemEventArgs>(DataSource_ItemNeeded);
            this.loopingListDataSource.ItemUpdated += new EventHandler<LoopingListDataItemEventArgs>(DataSource_ItemUpdated);
        }

        /// <summary>
        /// A collection for <see cref="LoopingListItemViewModel"/> objects.
        /// </summary>
        public LoopingListDataSource LoopingListDataSource
        {
            get
            {
                if (this.loopingListDataSource == null)
                {
                    this.InitializeLoopingListDataSource();
                }
                return this.loopingListDataSource;
            }
            private set
            {
                this.loopingListDataSource = value;
            }
        }

        /// <summary>
        /// Handles the ItemNeeded event of the DataSource control. The ItemNeeded event is raised whenever a data item instance is needed.
        /// </summary>
        void DataSource_ItemNeeded(object sender, LoopingListDataItemEventArgs e)
        {
            e.Item = new LoopingListItemViewModel()
            {
                ImageThumbnailSource = new Uri("Images/FrameThumbnail.png", UriKind.RelativeOrAbsolute),
            };
        }

        /// <summary>
        /// Handles the ItemUpdated event of the DataSource control. The ItemUpdated event is raised whenever a data item instance needs to be updated with new content.
        /// </summary>
        void DataSource_ItemUpdated(object sender, LoopingListDataItemEventArgs e)
        {
            (e.Item as LoopingListItemViewModel).ImageThumbnailSource = new Uri("Images/FrameThumbnail.png", UriKind.RelativeOrAbsolute);
        }
    }
}
