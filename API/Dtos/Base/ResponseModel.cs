using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos.BaseEntity.ResponseModel
{
    public class ResponseModel<T> where T : class
    {
        public ResponseModel()
        {
            this.Items = new List<T>();
            this.IsOk = true;
            
        }

        public T? Item { get; set; }
        public List<T>? Items { get; set; }
        public bool IsOk { get; set; }
        public string? Message { get; set; }
    }
}
