using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserWPF.ViewModel
{
   public class VMContainer
    {
        public Controller Controller { get; set; }
        public HistoryManager HistoryManager { get; set; }
        public VirtualKeyboardHandler VirtualKeyboardHandler { get; set; }

        public VMContainer(Controller controller, HistoryManager historyManager,
            VirtualKeyboardHandler virtualKeyboardHandler)
        {
            Controller = controller;
            HistoryManager = historyManager;
            VirtualKeyboardHandler = virtualKeyboardHandler;
        }
    }
}
