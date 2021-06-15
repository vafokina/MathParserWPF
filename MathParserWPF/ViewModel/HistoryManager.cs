using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using MathParserWPF.Model.Data;
using MathParserWPF.View;

namespace MathParserWPF.ViewModel
{
   public class HistoryManager : INotifyPropertyChanged
    {
        private readonly string _fileName;
        private readonly MainWindow _mainWindow;
        private ExpressionsList _expressions;

        // Конструктор
        public HistoryManager()
        {
            _fileName = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "MathParserData.xml");
            _mainWindow = (MainWindow)Application.Current.MainWindow;
            HistoryCellClickCommand = new DelegateCommand(HistoryCellClick);
            OpenHistory();
        }

        // Реализация ICommand
        public IDelegateCommand HistoryCellClickCommand { protected set; get; }

        // Команда вставки результата выражении
        // из истории в поле ввода
        private void HistoryCellClick(object param)
        {
            _mainWindow.Controller.VirtualKeyboardHandler.InputString += param.ToString();
        }

        // Добавление записи в историю
        public void AddNote(MathExpression expression)
        {
            _expressions.List.Add(expression);
            if (_expressions.List.Count > _mainWindow.HistoryListView.MaxCount) _expressions.List.RemoveAt(0);
            _mainWindow.HistoryListView.Add(expression.Source+ "\n= " + expression.Result, 
                HistoryCellClickCommand, expression.Result, this);
        }

        // Открытие из файла (извлечение информации)
        public void OpenHistory()
        {
            _expressions = new ExpressionsList();
            ExpressionsList temp;
            XmlSerializer formatter = new XmlSerializer(typeof(ExpressionsList));

            try
            {
                using (var fs = new FileStream(_fileName, FileMode.Open))
                {
                    temp = formatter.Deserialize(fs) as ExpressionsList;
                }

                if (temp != null)
                    foreach (MathExpression expr in temp.List)
                    {
                        AddNote(expr);
                    }
            }
            catch (FileNotFoundException) { }
        }

        // Сохранение в файл
        public void SaveHistory()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ExpressionsList));

            using (FileStream fs = new FileStream(_fileName, FileMode.Create))
            {
                formatter.Serialize(fs, _expressions);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
