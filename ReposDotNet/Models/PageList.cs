namespace ReposDotNet.Models
{
    public class PageList<T> : List<T>
    {
        const byte MaxSize = 10; // Maximum records to show on the page
        public byte Page { get; private set; } = 1; // Number of the page
        public byte Size { get; private set; } = 20;// Records number to show on the page
        public int RecordsNumber { get; private set; } 
        public int PagesQuantity { get; private set; }

        public PageList(byte size, byte page, int recordsNumber)
        {
            SetSize(size);
            SetPage(page);
            SetTotalRecords(recordsNumber);
            SetTotalPage();
        }

        public void SetPage(byte page)
        {
            if (page < 1)
                page = 1;

            Page = page;
        }

        public void SetTotalRecords(int value)
            => RecordsNumber = value;

        public void SetTotalPage()
            => PagesQuantity = (int)Math.Ceiling((decimal)RecordsNumber / Size);

        public void SetSize(byte size)
        {
            switch (size)
            {
                case < 1:
                    Size = 1;
                    break;

                case > MaxSize:
                    Size = MaxSize;
                    break;

                default:
                    Size = size;
                    break;
            }
        }

        public IEnumerable<T> GetRepos(IEnumerable<T> repos)
        {
            return repos.Skip((Page - 1) * Size)
                .Take(Size);
        }
    }
}
