﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using StockAnalyzer.Portofolio;
using System.IO;
using System.Windows;
using StockAnalyzer.StockClasses;

namespace StockAnalyzerApp.CustomControl.PortofolioDlgs
{
   public class ImportBinckOrderViewModel : NotifyPropertyChanged
   {
      public IEnumerable<string> Portfolios { get; set; }

      private string selectedPortfolio;
      public string SelectedPortfolio
      {
         get { return selectedPortfolio; }
         set
         {
            if (selectedPortfolio != value)
            {
               selectedPortfolio = value;
               this.ChangePortfolio();
               OnPropertyChanged("SelectedPortfolio");
            }
         }
      }

      private void ChangePortfolio()
      {
         StockPortofolio portfolio = StockAnalyzerForm.MainFrame.StockPortofolioList.First(p => p.Name == selectedPortfolio);

         this.Orders = new ObservableCollection<StockOrderViewModel>();
         foreach (var order in portfolio.OrderList)
         {
            this.orders.Add(new StockOrderViewModel(order));
         }

         this.orders.CollectionChanged += orders_CollectionChanged;
      }

      void orders_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
      {
         switch (e.Action)
         {
            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
               break;
            case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
               break;
            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
               StockPortofolio portfolio = StockAnalyzerForm.MainFrame.StockPortofolioList.First(p => p.Name == selectedPortfolio);
               portfolio.OrderList.Remove((e.OldItems[0] as StockOrderViewModel).Order);
               break;
            case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
               break;
            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
               break;
            default:
               break;
         }
      }

      private string text;
      public string Text
      {
         get { return text; }
         set
         {
            if (text != value)
            {
               text = value;
               OnPropertyChanged("Text");
            }
         }
      }

      private ObservableCollection<StockOrderViewModel> orders;
      public ObservableCollection<StockOrderViewModel> Orders
      {
         get { return orders; }
         set
         {
            if (orders != value)
            {
               orders = value;
               OnPropertyChanged("Orders");
            }
         }
      }

      public List<string> StockNames { get { return StockDictionary.StockDictionarySingleton.Keys.ToList(); } }


      public ImportBinckOrderViewModel()
      {
         this.Portfolios = StockAnalyzerForm.MainFrame.StockPortofolioList.Select(p => p.Name);
         this.selectedPortfolio = this.Portfolios.First();
         this.ChangePortfolio();
      }

      public void Import()
      {
         if (string.IsNullOrWhiteSpace(this.text)) return;

         using (StringReader stream = new StringReader(this.text))
         {
            try
            {
               string line;
               while ((line = stream.ReadLine()) != null)
               {
                  string[] fields = line.Split('\t');
                  int id = int.Parse(fields[0]);
                  if (!this.Orders.Any(o => o.ID == id) && fields[4] == "Exécuté")
                  {
                     StockOrder.OrderType type = fields[1] == "Achat" ? StockOrder.OrderType.BuyAtLimit : StockOrder.OrderType.SellAtLimit;
                     int qty = int.Parse(fields[5]);
                     float value = float.Parse(fields[6], StockAnalyzerForm.FrenchCulture);
                     DateTime date = DateTime.Parse(fields[7], StockAnalyzerForm.FrenchCulture);

                     string name = fields[3];
                     if (name.EndsWith(" SA.")) { name = name.Replace(" SA.", ""); }

                     if (mapping.ContainsKey(name))
                     {
                        name = mapping[name];
                     }
                     else if (name.EndsWith(" SA."))
                     {
                        name = name.Replace(" SA.", "");
                     }

                     StockOrder order = StockOrder.CreateExecutedOrder(id, name.ToUpper(), type, false, date, date, qty, value, qty * value > 1000f ? 5.0f : 2.5f);

                     StockPortofolio portfolio = StockAnalyzerForm.MainFrame.StockPortofolioList.First(p => p.Name == selectedPortfolio);

                     portfolio.OrderList.Add(order);
                     this.Orders.Add(new StockOrderViewModel(order));
                  }
               }
               this.ChangePortfolio();
            }
            catch(System.Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
         }
      }

      private SortedDictionary<string, string> mapping = new SortedDictionary<string, string>() { 
      { "Lyxor CAC 40 Daily Double Shrt UCITS ETF", "BX4" },
      { "Electricite de France (EDF)", "EDF" }
      };
   }
}
