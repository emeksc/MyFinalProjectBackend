using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Core.Entities.Concrete;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz eklendi";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductsListed = "Ürünler listelendi";
        public static string ProductCountOfCategoryError = "Kategorideki ürün sayısını aştınız.";
        public static string CategoryLimitExceded = "Kategorideki limit sayısını aştınız.";
        public static string AuthorizationDenied = "Yetkiniz yok.";

        public static string SuccessfulLogin = "Kullanıcı başarıyla giriş yaptı.";
        public static string UserAlreadyExists = "Kullanıcı zaten mevcut.";
        public static string AccessTokenCreated = "Token oluşturuldu.";
        public static User PasswordError { get; internal set; }
        public static User UserNotFound { get; internal set; }
        public static string UserRegistered = "Kullanıcı kaydedildi.";
    }
}
