//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shop.Ado
{
    using System;
    using System.Collections.Generic;
    
    public partial class Dish
    {
        public int Id { get; set; }
        public int Receipt_id { get; set; }
        public byte[] Image { get; set; }
    
        public virtual Receipt Receipt { get; set; }
    }
}
