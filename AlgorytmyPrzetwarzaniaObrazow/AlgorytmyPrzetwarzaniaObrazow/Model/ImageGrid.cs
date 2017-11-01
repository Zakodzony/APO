using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceGrid;

namespace AlgorytmyPrzetwarzaniaObrazow.Model
{
    public class ImageRows : RowsSimpleBase
    {
        private AutoSizeMode mAutoSizeMode = AutoSizeMode.Default;

        public ImageRows(ImageGrid grid)
            : base(grid)
        {
        }

        public new ImageGrid Grid
        {
            get { return (ImageGrid)base.Grid; }
        }

        public AutoSizeMode AutoSizeMode
        {
            get { return mAutoSizeMode; }
            set { mAutoSizeMode = value; }
        }

        public override int Count
        {
            get
            {
                if (Grid.DataSource == null)
                    return Grid.FixedRows;
                else
                    return Grid.DataSource.Height + Grid.FixedRows;
            }
        }

        public override AutoSizeMode GetAutoSizeMode(int row)
        {
            return mAutoSizeMode;
        }
    }

    public class ImageColumns : ColumnsSimpleBase
    {
        private AutoSizeMode mAutoSizeMode = AutoSizeMode.Default;

        public ImageColumns(ImageGrid grid)
            : base(grid)
        {
        }

        public new ImageGrid Grid
        {
            get { return (ImageGrid)base.Grid; }
        }

        public AutoSizeMode AutoSizeMode
        {
            get { return mAutoSizeMode; }
            set { mAutoSizeMode = value; }
        }

        public override int Count
        {
            get
            {
                if (Grid.DataSource == null)
                    return Grid.FixedColumns;
                else
                    return Grid.DataSource.Width + Grid.FixedColumns;
            }
        }

        public override AutoSizeMode GetAutoSizeMode(int row)
        {
            return mAutoSizeMode;
        }
    }

    public class ImageGrid : GridVirtual
    {
        private FastBitmap mDataSource = null;

        private SourceGrid.Cells.ICellVirtual mHeader = new ImageHeader();
        private SourceGrid.Cells.ICellVirtual mColumnHeader = new ImageColumnHeader();
        private SourceGrid.Cells.ICellVirtual mRowHeader = new ImageRowHeader();
        private SourceGrid.Cells.ICellVirtual mValueCell;

        public ImageGrid()
        {
            FixedRows = 1;
            FixedColumns = 1;
        }

        protected override ColumnsBase CreateColumnsObject()
        {
            return new ImageColumns(this);
        }

        protected override RowsBase CreateRowsObject()
        {
            return new ImageRows(this);
        }

        public override SourceGrid.Cells.ICellVirtual GetCell(int p_iRow, int p_iCol)
        {
            if (p_iRow < FixedRows && p_iCol < FixedColumns) return mHeader;
            else if (p_iRow < FixedRows) return mColumnHeader;
            else if (p_iCol < FixedColumns) return mRowHeader;
            else return mValueCell;
        }

        public new ImageRows Rows
        {
            get { return (ImageRows)base.Rows; }
        }

        public new ImageColumns Columns
        {
            get { return (ImageColumns)base.Columns; }
        }

        public FastBitmap DataSource
        {
            get { return mDataSource; }
            set
            {
                mDataSource = value;
                Bind();
            }
        }

        public SourceGrid.Cells.ICellVirtual Header
        {
            get { return mHeader; }
            set { mHeader = value; }
        }

        public SourceGrid.Cells.ICellVirtual ColumnHeader
        {
            get { return mColumnHeader; }
            set { mColumnHeader = value; }
        }

        public SourceGrid.Cells.ICellVirtual RowHeader
        {
            get { return mRowHeader; }
            set { mRowHeader = value; }
        }

        public SourceGrid.Cells.ICellVirtual ValueCell
        {
            get { return mValueCell; }
            set { mValueCell = value; }
        }

        public override bool EnableSort { get; set; }

        protected virtual void Bind()
        {
            ValueCell = null;
            if (mDataSource != null)
            {
                mValueCell = new SourceGrid.Cells.Virtual.CellVirtual();
                mValueCell.Model.AddModel(new ImageValueModel());
                mValueCell.Editor = new SourceGrid.Cells.Editors.TextBoxNumeric(typeof(byte));
                //ValueCell.Editor.MaximumValue = mDataSource.Levels - 1;
            }

            Rows.RowsChanged();
            Columns.ColumnsChanged();
        }
    }

    public class ImageValueModel : SourceGrid.Cells.Models.IValueModel
    {
        public object GetValue(CellContext cellContext)
        {
            FastBitmap bitmap = ((ImageGrid)cellContext.Grid).DataSource;
            int x = cellContext.Position.Column - cellContext.Grid.FixedColumns;
            int y = cellContext.Position.Row - cellContext.Grid.FixedRows;
            return bitmap[x, y];
        }

        public void SetValue(CellContext cellContext, object p_Value)
        {
            FastBitmap bitmap = ((ImageGrid)cellContext.Grid).DataSource;
            int x = cellContext.Position.Column - cellContext.Grid.FixedColumns;
            int y = cellContext.Position.Row - cellContext.Grid.FixedRows;
            byte value = (byte)p_Value;
            if (value < bitmap.Levels)
            {
                bitmap[x, y] = value;
                if (cellContext.Grid != null)
                    cellContext.Grid.Controller.OnValueChanged(cellContext, EventArgs.Empty);
            }
        }
    }

    public class ImageRowHeaderModel : SourceGrid.Cells.Models.IValueModel
    {
        public object GetValue(CellContext cellContext)
        {
            return cellContext.Position.Row - cellContext.Grid.FixedRows;
        }

        public void SetValue(CellContext cellContext, object p_Value)
        {
            throw new ApplicationException("Not supported");
        }
    }

    public class ImageColumnHeaderModel : SourceGrid.Cells.Models.IValueModel
    {
        public object GetValue(CellContext cellContext)
        {
            return cellContext.Position.Column - cellContext.Grid.FixedColumns;
        }

        public void SetValue(CellContext cellContext, object p_Value)
        {
            throw new ApplicationException("Not supported");
        }
    }

    public class ImageColumnHeader : SourceGrid.Cells.Virtual.ColumnHeader
    {
        public ImageColumnHeader()
        {
            Model.AddModel(new ImageColumnHeaderModel());
            AutomaticSortEnabled = false;
            ResizeEnabled = false;
        }
    }

    public class ImageRowHeader : SourceGrid.Cells.Virtual.RowHeader
    {
        public ImageRowHeader()
        {
            Model.AddModel(new ImageRowHeaderModel());
            ResizeEnabled = false;
        }
    }

    public class ImageHeader : SourceGrid.Cells.Virtual.Header
    {
        public ImageHeader()
        {
            Model.AddModel(new SourceGrid.Cells.Models.NullValueModel());
        }
    }
}
