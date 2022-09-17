using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace BatchRename
{
    public class Method
    {
        public static StackPanel CreateDropDownMethod(string name)
        {
            // DropDownButton as method
            DropDownButton ddBtn = new DropDownButton();
            ddBtn.Name = "ddBtn";
            ddBtn.Width = 258;
            ddBtn.Height = 43;
            ddBtn.Margin = new Thickness(-3, 0, 0, 0);
            ddBtn.Background = Brushes.White;

            // content of ddBtn
            Canvas cvsBtnContent = new Canvas();
            ddBtn.Content = cvsBtnContent;

            Button btnDelete = new Button();
            btnDelete.Content = "X";
            btnDelete.Width = 20;
            cvsBtnContent.Children.Add(btnDelete);
            Canvas.SetLeft(btnDelete, -111);
            Canvas.SetTop(btnDelete, -12);
            btnDelete.Click += ddBtnDeleteClick;

            CheckBox cb = new CheckBox();
            cb.Name = "checkBox";
            cb.IsChecked = true;
            cvsBtnContent.Children.Add(cb);
            Canvas.SetLeft(cb, -80);
            Canvas.SetTop(cb, -7);
            cb.Checked += ddBtnCheckBox_Checked;
            cb.Unchecked += ddBtnCheckBox_Unchecked;

            Label lb = new Label();
            lb.Name = "labelMethod";
            lb.Content = name;
            lb.Width = 165;
            cvsBtnContent.Children.Add(lb);
            Canvas.SetLeft(lb, -53);
            Canvas.SetTop(lb, -14);

            // Drop down content
            CreateDropDownContent(ddBtn, name);

            // add ddBtn to a StackPanel
            StackPanel spListBoxMethodItem = new StackPanel();
            spListBoxMethodItem.Children.Add(ddBtn);

            return spListBoxMethodItem;
        }

        public static StackPanel CreateSimpleMethod(string name)
        {
            // DropDownButton as method
            Button btn = new Button();
            btn.Name = "btn";
            btn.Width = 258;
            btn.Height = 43;
            btn.Margin = new Thickness(-3, 0, 0, 0);
            btn.Background = Brushes.White;

            // content of ddBtn
            Canvas cvsBtnContent = new Canvas();
            btn.Content = cvsBtnContent;

            Button btnDelete = new Button();
            btnDelete.Content = "X";
            btnDelete.Width = 20;
            cvsBtnContent.Children.Add(btnDelete);
            Canvas.SetLeft(btnDelete, -119);
            Canvas.SetTop(btnDelete, -12);
            btnDelete.Click += btnDeleteClick;

            CheckBox cb = new CheckBox();
            cb.Name = "checkBox";
            cb.IsChecked = true;
            cvsBtnContent.Children.Add(cb);
            Canvas.SetLeft(cb, -88);
            Canvas.SetTop(cb, -7);
            cb.Checked += btnCheckBox_Checked;
            cb.Unchecked += btnCheckBox_Unchecked;

            Label lb = new Label();
            lb.Name = "labelMethod";
            lb.Content = name;
            lb.Width = 165;
            cvsBtnContent.Children.Add(lb);
            Canvas.SetLeft(lb, -61);
            Canvas.SetTop(lb, -14);

            // add ddBtn to a StackPanel
            StackPanel spListBoxMethodItem = new StackPanel();
            spListBoxMethodItem.Children.Add(btn);

            return spListBoxMethodItem;
        }

        private static void CreateDropDownContent(DropDownButton method, string name)
        {
            switch (name)
            {
                case "Replace":
                    CreateReplaceDropDownContent(method);
                    break;

                case "New Case":
                    CreateNewCaseDropDownContent(method);
                    break;

                case "Move":
                    CreateMoveDropDownContent(method);
                    break;
            }
        }

        private static void CreateReplaceDropDownContent(DropDownButton method)
        {
            StackPanel spParent = new StackPanel();
            spParent.Width = 255;
            method.DropDownContent = spParent;

            // text replaced
            StackPanel spChild1 = new StackPanel();
            spChild1.Orientation = Orientation.Vertical;
            spParent.Children.Add(spChild1);

            Label lbTextReplaced = new Label();
            lbTextReplaced.Content = "Text replaced: ";

            TextBox tbTextReplaced = new TextBox();
            tbTextReplaced.Name = "tbTextReplaced";

            spChild1.Children.Add(lbTextReplaced);
            spChild1.Children.Add(tbTextReplaced);

            // replace with
            StackPanel spChild2 = new StackPanel();
            spChild2.Orientation = Orientation.Vertical;
            spParent.Children.Add(spChild2);

            Label lbReplaceWith = new Label();
            lbReplaceWith.Content = "Replace with: ";

            TextBox tbReplaceWith = new TextBox();
            tbReplaceWith.Name = "tbReplaceWith";

            spChild2.Children.Add(lbReplaceWith);
            spChild2.Children.Add(tbReplaceWith);
        }

        private static void CreateNewCaseDropDownContent(DropDownButton method)
        {
            StackPanel sp = new StackPanel();
            sp.Name = "spdd";
            sp.Width = 255;
            method.DropDownContent = sp;

            RadioButton radUpper = new RadioButton();
            radUpper.Name = "radUpper";
            radUpper.Content = "Set upper case";
            radUpper.IsChecked = true;

            RadioButton radLower = new RadioButton();
            radLower.Name = "radLower";
            radLower.Content = "Set lower case";

            RadioButton radUpperFirst = new RadioButton();
            radUpperFirst.Name = "radUpperFirst";
            radUpperFirst.Content = "Set upper case first letter in every word";

            sp.Children.Add(radUpper);
            sp.Children.Add(radLower);
            sp.Children.Add(radUpperFirst);
        }

        private static void CreateMoveDropDownContent(DropDownButton method)
        {
            StackPanel spParent = new StackPanel();
            spParent.Width = 255;
            method.DropDownContent = spParent;

            // move from
            StackPanel spChild1 = new StackPanel();
            spChild1.Orientation = Orientation.Vertical;
            spParent.Children.Add(spChild1);

            Label lbMoveFrom = new Label();
            lbMoveFrom.Content = "Move from: ";

            TextBox tbMoveFrom = new TextBox();
            tbMoveFrom.Name = "tbMoveFrom";
            tbMoveFrom.PreviewTextInput += TbMoveFrom_PreviewTextInput;

            spChild1.Children.Add(lbMoveFrom);
            spChild1.Children.Add(tbMoveFrom);

            // move count
            StackPanel spChild2 = new StackPanel();
            spChild2.Orientation = Orientation.Vertical;
            spParent.Children.Add(spChild2);

            Label lbMoveCount = new Label();
            lbMoveCount.Content = "Move count: ";

            TextBox tbMoveCount = new TextBox();
            tbMoveCount.Name = "tbMoveCount";
            tbMoveCount.PreviewTextInput += TbMoveCount_PreviewTextInput;

            spChild2.Children.Add(lbMoveCount);
            spChild2.Children.Add(tbMoveCount);

            // move to
            StackPanel spChild3 = new StackPanel();
            spChild3.Orientation = Orientation.Vertical;
            spParent.Children.Add(spChild3);

            Label lbMoveTo = new Label();
            lbMoveTo.Content = "Move to: ";

            TextBox tbMoveTo = new TextBox();
            tbMoveTo.Name = "tbMoveTo";
            tbMoveTo.PreviewTextInput += TbMoveTo_PreviewTextInput;

            spChild3.Children.Add(lbMoveTo);
            spChild3.Children.Add(tbMoveTo);
        }

        private static void TbMoveTo_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            int result = 0;

            if (!int.TryParse(e.Text, out result))
            {
                e.Handled = true;
                System.Windows.MessageBox.Show("Please enter a positive integer!", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private static void TbMoveCount_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            int result = 0;

            if (!int.TryParse(e.Text, out result))
            {
                e.Handled = true;
                System.Windows.MessageBox.Show("Please enter a positive integer!", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private static void TbMoveFrom_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            int result = 0;

            if (!int.TryParse(e.Text, out result))
            {
                e.Handled = true;
                System.Windows.MessageBox.Show("Please enter a positive integer!", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// delete button - method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ddBtnDeleteClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Canvas cvs = btn.Parent as Canvas;
            DropDownButton ddbtn = cvs.Parent as DropDownButton;
            StackPanel sp = ddbtn.Parent as StackPanel;
            sp.Children.Remove(ddbtn);
        }

        private static void ddBtnCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Canvas cvs = cb.Parent as Canvas;
            DropDownButton ddbtn = cvs.Parent as DropDownButton;
            ddbtn.Background = Brushes.Red;
        }

        private static void ddBtnCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Canvas cvs = cb.Parent as Canvas;
            DropDownButton ddbtn = cvs.Parent as DropDownButton;
            ddbtn.Background = Brushes.White;
        }

        private static void btnDeleteClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Canvas cvs = btn.Parent as Canvas;
            Button ddbtn = cvs.Parent as Button;
            StackPanel sp = ddbtn.Parent as StackPanel;
            sp.Children.Remove(ddbtn);
        }

        private static void btnCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Canvas cvs = cb.Parent as Canvas;
            Button ddbtn = cvs.Parent as Button;
            ddbtn.Background = Brushes.Red;
        }

        private static void btnCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Canvas cvs = cb.Parent as Canvas;
            Button ddbtn = cvs.Parent as Button;
            ddbtn.Background = Brushes.White;
        }
    }
}
