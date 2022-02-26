using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string ProductsListed = "Ürünler listelendi";
        public static string MaintenanceTime = "Bakım saati";
        public static string ProductCategoryLimitExceeded = "Eklemeye çalıştığınız ürün kategorisinde en fazla 10 ürün olabilir";
        public static string CategoryLimitExceeded = "Maksimum 15 kategori olabilir";
        public static string ProductNameExist = "Eklemeye çalıştığınız ürün ismi mevcut";
        public static string AuthorizationDenied = "Yetkiniz yok";
    }
}
