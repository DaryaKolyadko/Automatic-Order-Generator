using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace AutomaticOrderGeneration
{
    class OrderFileGenerator
    {
        public enum Filials
        {
            Minsk = 0,
            MarGorka,
            Volkovisk,
            Luninec,
            Smorgon
        }

        public static String[] filialNames = { "Минск", "Марьина Горка", "Волковыск", "Лунинец", 
                                                 "Сморгонь" };

        private static String[] filialShortNames = { "Мн", "Мг", "В", "Л", "С" };

        private static String[] filialCodes = { "153001", "153000", "153011", "153101", "153111" };
        private static String[] filialAdditionalCodes = { "", "153001", "", "150501", "152111" };
        private static String[] filialSpecialAccounts = { "", "3140186450061", "", "3140100020021", "3140000000101" }; // если correspondentAccount совпадает с этим кодом, то используем filialAdditionalCodes
        private static String[] indents = {"{0, 28}", "{0, 21}"}; // отступ вправо, отступ влево
        private static String standard = "{0, 26}"; // без отступов
        private static int previousWasMoved = 0;
        private static String indent;

        public static void SaveOrderIntoFile(string prologue, List<PaymentRecord> content, List<List<int>> filialMarks,
            string epilogue)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt";
            dialog.RestoreDirectory = true;
            dialog.FileName = MainForm.fileName.Remove(MainForm.fileName.Length - 4) + "_П_";

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
    
            File.WriteAllText(dialog.FileName, prologue, Encoding.Default);

            List<String> lines = new List<String>();
            int count = content.Count;
            
            for (int i = 0; i < count; i++)
            {
                if (filialMarks[i].Count == 1)
                {
                    int filialIndex = Array.IndexOf(filialSpecialAccounts, content[i].correspondentAccount);

                    if (filialIndex != -1)
                    {
                        lines.Add(ChangedPaymentRecord(content[i],filialIndex, true));
                    }
                    else
                        lines.Add(ChangedPaymentRecord(content[i], filialMarks[i].First(), false));
                }
                else
                {
                    int filialCount = filialMarks[i].Count;

                    for (int j = 0; j < filialCount; j++)
                    {
                        if (j == 0)
                            lines.Add(ChangedPaymentRecord(content[i], filialMarks[i][j], true, true, false));
                        else
                            lines.Add(ChangedPaymentRecord(content[i], filialMarks[i][j], true, false, false));
                    }
                }
            }

            String allLines = "";

            foreach (String line in lines)
                allLines += line + "\r\n";

            File.AppendAllText(dialog.FileName, allLines, Encoding.Default);
            File.AppendAllText(dialog.FileName, epilogue, Encoding.Default);
        }

        private static String ChangedPaymentRecord(PaymentRecord record, int filial, bool additionalCode)
        {
            return ChangedPaymentRecord(record, filial, false, false, additionalCode);
        }
        private static String ChangedPaymentRecord(PaymentRecord record, int filial, bool complex, bool firstInComplex, 
            bool additionalCode)
        {
            string result = String.Format("{0, -12}{1, 7}{2, 3}",
                record.documentDate.ToString("dd.MM.yyyy"), record.documentNumber,
                record.operationCode);

            if (additionalCode)
            {
                result += String.Format("{0, 10}", filialAdditionalCodes[filial]);
            }
            else
            {
                result += String.Format("{0, 10}", filialCodes[filial]);
            }

            result += String.Format("{0}{1, 15}{2, 29}", record.correspondentCode, 
                record.correspondentAccount, record.ratingDebit);

            if ((filial == (int)Filials.Minsk && !complex) || additionalCode)
            {
                result += String.Format(standard, record.ratingCredit);

                if (previousWasMoved == 1)
                    previousWasMoved = 0;

                return result;
            }

            if (firstInComplex || !complex)
            {
                indent = indents[previousWasMoved];
                previousWasMoved++;
                previousWasMoved %= 2;
            }

            result += String.Format(indent + " {1}", record.ratingCredit, filialShortNames[filial]);
            
            return result;
        }
    }
}