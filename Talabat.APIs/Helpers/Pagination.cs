using Talabat.APIs.DTOS;

namespace Talabat.APIs.Helpers
{
    public class Pagination<T>
    {
       

        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int Count { get; set; }

        public IReadOnlyList<T> Data { get; set; }

        public Pagination(int index,int c, int v, IReadOnlyList<T> data)
        {
            PageIndex = index;
            PageSize = v;
            Data = data;
            Count = c;
        }

    }
}
