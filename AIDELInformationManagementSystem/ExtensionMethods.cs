using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIDELInformationManagementSystem
{
    public static class StringExtension
    {
        public static T ToCorrespondingEnumValue<T>(this string _string)
        {
            if (_string == null || _string == "")
                return default;

            if (typeof(T).IsEnum)
            {
                foreach (T e in Enum.GetValues(typeof(T)))
                {
                    if (string.Compare(_string, e.ToString(), StringComparison.OrdinalIgnoreCase) == 0) //...if both strings are equal (ignoring the case)
                        return e;
                }
            }

            return default;
        }
    }

    public static class DictionaryExtension
    {
        public static K GetFirstKey<K, V>(this Dictionary<K, V> _dictionary, V _value) { return _dictionary.First(x => x.Value.Equals(_value)).Key; }

        public static K GetFirstKeyOrDefault<K, V>(this Dictionary<K, V> _dictionary, V _value)
        {
            if (_dictionary == null)
                return default;

            return _dictionary.FirstOrDefault(x => x.Value.Equals(_value)).Key;
        }

        public static bool RemoveRange<K, V>(this Dictionary<K, V> _dictionary, Dictionary<K, V> _range)
        {
            if (_dictionary == null || _range == null)
                return false;

            // Check whether all elements in _range are also in _dictionary
            foreach (var element in _range)
            {
                if (!_dictionary.Contains(element))
                    return false;
            }

            // Remove elements from _dictionary
            foreach (var element in _range)
            {
                _dictionary.Remove(element.Key);
            }

            return true;
        }
    }

    public static class FormExtension
    {
        public static void SwitchTo(this Form _form, Form _targetForm)
        {
            _form.Hide();
            _targetForm.FormClosed += (_sender, _e) => _form.Close();
            _targetForm.StartPosition = FormStartPosition.Manual;
            _targetForm.Location = _form.Location;
            _targetForm.Show();
        }

        public static void LogAndSwitchTo(this Form _form, Form _targetForm)
        {
            DataContainer.Instance.PreviousFormLog.Add(_form);

            _form.SwitchTo(_targetForm);
        }

        public static void SwitchToPrevious(this Form _form)
        {
            var log = DataContainer.Instance.PreviousFormLog;

            _form.SwitchTo(log.Last());

            log.RemoveAt(log.Count - 1);
        }
    }

    public static class DataGridViewExtension
    {
        public static bool AllRowsEqual(this DataGridView _dataGridView, DataGridView _targetDgv)
        {
            var rows = _dataGridView.Rows;
            var targetRows = _targetDgv.Rows;

            if (rows.Count != targetRows.Count)
                return false;

            foreach (DataGridViewRow row in rows)
            {
                if (!targetRows.Any(row))
                    return false;
            }

            return true;
        }
    }

    public static class DataGridViewRowCollectionExtension
    {
        public static bool Any(this DataGridViewRowCollection _dataGridViewRowCollection, DataGridViewRow _targetRow)
        {
            foreach (DataGridViewRow row in _dataGridViewRowCollection)
            {
                if (row.AllCellsEqual(_targetRow))
                    return true;
            }

            return false;
        }

        public static void Load(this DataGridViewRowCollection _dataGridViewRowCollection, DataGridViewRowCollection _targetRows)
        {
            foreach (DataGridViewRow row in _targetRows)
            {
                DataGridViewRow copy = (DataGridViewRow)(row.Clone());

                int cellIndex = 0;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    copy.Cells[cellIndex].Value = cell.Value;
                    cellIndex++;
                }

                _dataGridViewRowCollection.Add(copy);
            }
        }
    }

    public static class DataGridViewRowExtension
    {
        public static bool AllCellsEqual(this DataGridViewRow _dataGridViewRow, DataGridViewRow _targetRow)
        {
            var cells = _dataGridViewRow.Cells;
            var targetCells = _targetRow.Cells;

            if (cells.Count != targetCells.Count)
                return false;

            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i].Value != targetCells[i].Value)
                    return false;

                if (cells[i].Style.BackColor != targetCells[i].Style.BackColor)
                    return false;
            }

            return true;
        }

        public static int CopyTo(this DataGridViewRow _row, DataGridView _targetDgv, int _numOfCellsToCopy = -1)
        {
            var cells = _row.Cells;

            int numOfCells = cells.Count;
            if (_numOfCellsToCopy > 0 && _numOfCellsToCopy < numOfCells)
                numOfCells = _numOfCellsToCopy;
            object[] cellValues = new object[numOfCells];
            for (int i = 0; i < numOfCells; i++)
            {
                cellValues[i] = cells[i].Value;
            }

            int newRowIndex = _targetDgv.Rows.Add(cellValues);
            for (int i = 0; i < numOfCells; i++)
            {
                _targetDgv.Rows[newRowIndex].Cells[i].Style.BackColor = cells[i].Style.BackColor;
            }

            return newRowIndex;
        }

        public static int MoveTo(this DataGridViewRow _row, DataGridView _targetDgv, int _numOfCellsToMove = -1)
        {
            // Copy row to target table
            int newRowIndex = _row.CopyTo(_targetDgv, _numOfCellsToMove);

            // Remove row from this table
            _row.DataGridView.Rows.Remove(_row);

            return newRowIndex;
        }
    }

    public static class CellEnumerableExtension
    {
        public static Cell GetCell(this IEnumerable<Cell> _cells, string _cellReference)
        {
            return _cells.FirstOrDefault(x => x.CellReference == _cellReference);
        }

        public static Cell GetCellForColumn(this IEnumerable<Cell> _cells, string _columnName)
        {
            return _cells.FirstOrDefault(x => GetColumnName(x.CellReference) == _columnName);
        }

        private static string GetColumnName(string _cellReference)
        {
            // Create a regular expression to match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(_cellReference);
            return match.Value;
        }
    }
}
