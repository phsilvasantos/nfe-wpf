﻿using EmissorNFe.ViewModel;
using NFe.WPF.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NFe.WPF.View.NotaFiscal
{
   /// <summary>
   /// Interaction logic for EnviarEmailWindow.xaml
   /// </summary>
   public partial class EnviarEmailWindow : Window, IClosable
   {
      public EnviarEmailWindow()
      {
         InitializeComponent();
      }
   }
}
