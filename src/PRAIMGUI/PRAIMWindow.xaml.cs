﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using PRAIM.SnapshotManager;


namespace PRAIM
{
    /// <summary>
    /// Interaction logic for PRAIMWindow.xaml
    /// </summary>
    public partial class PRAIMWindow : Window
    {
        public ActionItem actionItem;
        public ActionMetaData metaData;
        
        public PRAIMViewModel ViewModel { get { return this.DataContext as PRAIMViewModel; } }

        public ICollectionView DummyDBItems { get; private set; }


        public PRAIMWindow()
        {
            metaData = new ActionMetaData();
            actionItem = new ActionItem();
            List<ActionMetaData> dummyDBItems = new List<ActionMetaData> {
                new ActionMetaData { DateTime = new DateTime(2010,10,10), Comments = "Comment #1", Priority=Priority.High, ProjectID = 1, Version = 1.1 },
                new ActionMetaData { DateTime = new DateTime(2010,10,10), Comments = "Comment #2", Priority=Priority.High, ProjectID = 1, Version = 1.1 },
                new ActionMetaData { DateTime = new DateTime(2010,10,10), Comments = "Comment #3", Priority=Priority.High, ProjectID = 1, Version = 1.1 },
                new ActionMetaData { DateTime = new DateTime(2010,10,10), Comments = "Comment #4", Priority=Priority.High, ProjectID = 1, Version = 1.1 },
                new ActionMetaData { DateTime = new DateTime(2010,10,10), Comments = "Comment #5", Priority=Priority.High, ProjectID = 1, Version = 1.1 },
                new ActionMetaData { DateTime = new DateTime(2010,10,10), Comments = "Comment #6", Priority=Priority.High, ProjectID = 1, Version = 1.1 },
            };

            DummyDBItems = CollectionViewSource.GetDefaultView(dummyDBItems);
            InitializeComponent();

            this.DataContext = new PRAIMViewModel(1, "1.0", Priority.Low);
        }

        private void OnTakeSnapshot(object sender, RoutedEventArgs e)
        {
            this.LayoutUpdated += RunSnapshotMgr;
            this.Hide();
        }

        private void RunSnapshotMgr(object sender, EventArgs e)
        {
            if (IsVisible == true) return;
            Thread.Sleep(200);

            SnapshotManagerWindow snapshotMgr = new SnapshotManagerWindow();
            snapshotMgr.Closed += SnapshotMgrClosed;
            snapshotMgr.Show();

            this.LayoutUpdated -= RunSnapshotMgr;
        }


        private void SnapshotMgrClosed(object sender, EventArgs e)
        {
            SnapshotManagerWindow snapshotMgr = sender as SnapshotManagerWindow;
            this.Show();
            ViewModel.CroppedImage = snapshotMgr.CroppedImage;
        }


        private void OnSave(object sender, RoutedEventArgs e)
        {
        }

        private void searchComments(object sender, TextChangedEventArgs e)
        {
            metaData.Comments = sender as string;
        }

        private void searchPriority(object sender, SelectionChangedEventArgs e)
        {
            //metaData.Priority = (Priority)sender;
        }

        private void searchVersion(object sender, TextChangedEventArgs e)
        {
            //metaData.Version = (double)sender;
        }

        private void searchProjectID(object sender, TextChangedEventArgs e)
        {
            //metaData.ProjectID = (int)sender;
        }

        private void searchButton(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
