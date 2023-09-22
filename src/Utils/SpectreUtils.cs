using Spectre.Console;

namespace Chinook.Src.Utils
{
    class SpectreUtils
    {
        public static Table CreateTable(
            string[] columns,
            List<string[]> rows,
            string title = "Table"
            )
        {
            var table = new Table
            {
                Title = new TableTitle(title),
            };

            table.DoubleBorder();

            for (int i = 0; i < columns.Length; i++)
            {
                table.AddColumn(columns[i]);
            }

            foreach (string[] row in rows)
            {
                table.AddRow(row);
            }

            return table;
        }
    }
}