using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using MathParserWPF.Model;
using MathParserWPF.Model.Data;
using MathParserWPF.View;
using Microsoft.Win32;

namespace MathParserWPF.ViewModel
{
   public class HistoryManager : INotifyPropertyChanged
    {
        private string fileName;
        private MainWindow _mainWindow;
        private ExpressionsList expressions;

        public HistoryManager()
        {
            fileName = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "DateLinks.xml");
            _mainWindow = (MainWindow)Application.Current.MainWindow;
            HistoryCellClickCommand = new DelegateCommand(HistoryCellClick);
            OpenHistory();
        }

        // Реализация ICommand
        public IDelegateCommand HistoryCellClickCommand { protected set; get; }


        private void HistoryCellClick(object param)
        {
            _mainWindow.Input.Text += param.ToString();
        }

        public void AddNote(MathExpression expression)
        {
            expressions.List.Add(expression);
            if (expressions.List.Count > _mainWindow.HistoryListView.MaxCount) expressions.List.RemoveAt(0);
            _mainWindow.HistoryListView.Add(expression.Source+ "\n" + expression.Result, 
                HistoryCellClickCommand, expression.Result, _mainWindow.HistoryManager);
        }

        public void OpenHistory()
        {
            expressions = new ExpressionsList();
            ExpressionsList temp = new ExpressionsList();
            XmlSerializer formatter = new XmlSerializer(typeof(ExpressionsList));

            try
            {
                using (var fs = new FileStream(fileName, FileMode.Open))
                {
                    temp = formatter.Deserialize(fs) as ExpressionsList;
                }
                foreach (MathExpression expr in temp.List)
                {
                    AddNote(expr);
                }
            }
            catch (FileNotFoundException e) { }
        }

        public void SaveHistory()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ExpressionsList));

            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                formatter.Serialize(fs, expressions);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
