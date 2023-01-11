using GameStore.Domain.Helper;
using GameStore.Domain.Identity;
using GameStore.Domain.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Infrastructure
{
    public class IdentityDbInitializer : DropCreateDatabaseIfModelChanges<GameStoreDBContext>
    {
        protected override void Seed(GameStoreDBContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        public void PerformInitialSetup(GameStoreDBContext context)
        {
            GetRoles().ForEach(c => context.Roles.Add(c));
            GetCategories().ForEach(c => context.Categories.Add(c));
            GetProducts().ForEach(c => context.Products.Add(c));
            context.SaveChanges();
            GameStorePasswordHasher hasher = new GameStorePasswordHasher();
            var user = new AppUser { UserName = "admin", Email = "admin@zoovostorg.com", PasswordHash = hasher.HashPassword("admin"), Membership = "Admin" };
            var role = context.Roles.Where(r => r.Name == "Admin").First();
            user.Roles.Add(new IdentityUserRole { RoleId = role.Id, UserId = user.Id });
            context.Users.Add(user);
            user = new AppUser { UserName = "regular", Email = "regular@zoovostorg.com", PasswordHash = hasher.HashPassword("regular"), Membership = "Regular" };
            role = context.Roles.Where(r => r.Name == "Regular").First();
            user.Roles.Add(new IdentityUserRole { RoleId = role.Id, UserId = user.Id });
            context.Users.Add(user);
            user = new AppUser { UserName = "advanced", Email = "advanced@zoovostorg.com", PasswordHash = hasher.HashPassword("advanced"), Membership = "Advanced" };
            role = context.Roles.Where(r => r.Name == "Advanced").First();
            user.Roles.Add(new IdentityUserRole { RoleId = role.Id, UserId = user.Id });
            context.Users.Add(user);
            context.SaveChanges();
        }

        private static List<AppRole> GetRoles()
        {
            var roles = new List<AppRole> {
               new AppRole {Name="Admin", Description="Admin"},
               new AppRole {Name="Regular", Description="Regular"},
               new AppRole {Name="Advanced", Description="Advanced"}
            };

            return roles;
        }

        private static List<Category> GetCategories()
        {
            var categories = new List<Category> {
               new Category {CategoryId=1, CategoryName="Dog"},
               new Category {CategoryId=2, CategoryName="Cat"},
               new Category {CategoryId=3, CategoryName="Other"}
            };

            return categories;
        }

        private static List<Product> GetProducts()
        {
            var products = new List<Product> {
               new Product {ProductId=1, ProductName="Eukanuba паучи для щенков", CategoryId = 1, Price = 80.00, Image="dogs/d1.jpg", Condition="Способствует поддержанию иммунной системы за счет антиоксидантов.", Discount=10, UserId="Admin"},
               new Product {ProductId=2, ProductName="Purina One контроль веса", CategoryId = 1, Price = 34.00, Image="dogs/d2.jpg", Condition="Разрабатывая высококачественные влажные корма, эксперты Purina One создали для Вашего питомца особый рацион с учетом его потребностей.", Discount=10, UserId="Admin"},
               new Product {ProductId=3, ProductName="Tappi одежда комбинезон", CategoryId = 1, Price = 2190.00, Image="dogs/d3.jpg", Condition="Созданный специально для шпицев, комбинезон 'Фифти' оснащен непромокаемыми манжетами для сохранения чистоты лап и шерсти, мембранная ткань надежно сохранит тепло питомца, атласная ткань подкладки предотвратит спутывание шерсти, а комфортные кнопки на спинке сделают подготовку к прогулке максимально быстрой и удобной.", Discount=10, UserId="Admin"},
               new Product {ProductId=4, ProductName="PetshopRu домик-лежанка Витани", CategoryId = 1, Price = 1949.00, Image="dogs/d4.jpg", Condition="Позвольте своему питомцу ощутить настоящий комфорт и блаженство во время отдыха. Приятный материал и стильный дизайн лежанки подарят радость вам и вашему любимцу на долгое время.", Discount=10, UserId="Admin"},
               new Product {ProductId=5, ProductName="Фармакс фармавит Актив", CategoryId = 1, Price = 469.00, Image="dogs/d5.jpg", Condition="Витаминно-минеральный комплекс нового поколения с фруктовыми энзимами. Поддержание баланса витаминов, аминокислот, минеральных веществ в организме взрослых собак.", Discount=10, UserId="Admin"},
               new Product {ProductId=6, ProductName="Rogz игрушка-кормушка", CategoryId = 1, Price = 899.99, Image="dogs/d6.jpg", Condition="Эта игрушка-кормушка меняет представление о медленном кормлении! 3 уровня сложности для различных уровней обучения. Заполните игрушку кормом или лакомствами и наблюдайте за увлекательной игрой вашего питомца. Игрушка стимулирует мышление собаки и помогает питаться правильно. ", Discount=10, UserId="Admin"},
               
               new Product {ProductId=7, ProductName="ROYAL CANIN Sterilised", CategoryId = 2, Price = 93.00, Image="cats/c1.jpg", Condition="Благодаря контролируемому уровню калорийности и умеренному содержанию жиров ROYAL CANIN® Sterilised в соусе помогает кошке не набирать избыточный вес (при условии, что при кормлении не превышается рекомендуемый объем порций).", Discount=10, UserId="Admin"},
               new Product {ProductId=8, ProductName="FOXIE Colour Pastel", CategoryId = 2, Price = 2590.00, Image="cats/c2.jpg", Condition="Наполнитель лежака - полиэфирное волокно, которое долго сохраняет прочность, не мнется и быстро высыхает. Обтекаемая форма и бортики гарантируют комфорт использования. Съемный матрас-вставку можно стирать. Размер лежака: 60х50х18 см. Цвет: песочно-бежевый.", Discount=10, UserId="Admin"},
               new Product {ProductId=9, ProductName="TRIXIE Мяч веревочный", CategoryId = 2, Price = 199.99, Image="cats/c3.jpg", Condition="Во время игры с кошкой мячик может испачкаться, в том числе и слюной, а благодаря веревке ваши руки всегда останутся чистыми.", Discount=10, UserId="Admin"},
               new Product {ProductId=10, ProductName="TRIXIE Мышь заводная", CategoryId = 2, Price = 339.99, Image="cats/c4.jpg", Condition="TRIXIE Игрушка 'Мышь заводная' 7 см", Discount=10, UserId="Admin"},
               new Product {ProductId=11, ProductName="FOXIE Дикий леопард", CategoryId = 2, Price = 1116.99, Image="cats/c5.jfif", Condition="Игровой тоннель для кошек FOXIE - это отличный тренажер и место игр для Вашего питомца. Шуршащий материал тоннеля привлечет внимание кошки, вызвав у нее желание играть и бегать. Сетчатое смотровое отверстие предоставит кошке свободу движения внутри. Длина тоннеля оптимальна для нескольких животных. Тоннель удобно хранить в сложенном виде. Подходит для использования как в помещении, так и на улице. Размер тоннеля: 25х90 см.", Discount=10, UserId="Admin"},
               new Product {ProductId=12, ProductName="PRO PLAN", CategoryId = 2, Price = 529.99, Image="cats/c6.jpg", Condition="После семи лет кошка входит в почтенный возраст. Ее организм становится более уязвимым: повышается риск заболеваний, снижается когнитивная функция. Дополнительно к особым потребностям, связанным со стерилизацией, появляются также и возрастные особенности здоровья. На этом этапе очень важно поддержать Вашего питомца и скорректировать его рацион. Эксперты PRO PLAN разработали сухой корм для стерилизованных кошек с уникальной формулой, которая помогает сохранить и продлить жизни пожилых питомцев", Discount=10, UserId="Admin"},
               new Product {ProductId=13, ProductName="Когтеточка Мышь", CategoryId = 2, Price =  599.99, Image="cats/c7.jpg", Condition="Когтеточка Мышь для кошек от Petmax предназначена для того, чтобы удовлетворить естественную потребность кошки в заточке когтей.", Discount=10, UserId="Admin"},
               new Product {ProductId=14, ProductName="Дана Ультра ошейник", CategoryId = 2, Price = 257.99, Image="cats/c8.jpg", Condition="Дана Ультра ошейник инсектоакарицидный для кошек, 35 см.", Discount=10, UserId="Admin"},
               new Product {ProductId=15, ProductName="Triol автокормушка", CategoryId = 2, Price = 654.99, Image="cats/c9.jpg", Condition="Использование такой автокормушки решит проблему кормления питомца во время долгого отсутствия хозяев. Все элементы конструкции выполнены из качественного пластика и легко разбираются на составные части, что очень удобно при гигиенической обработке и транспортировке изделия. Объем 350мл. Размер 215х130х185 мм.", Discount=10, UserId="Admin"},
               new Product {ProductId=16, ProductName="Переноска Спутник-2", CategoryId = 2, Price = 3000.00, Image="cats/c10.jpg", Condition="Переноска пластиковая Спутник предназначена для транспортировки животных, поставляется вместе с чехлом-кофром и наплечным ремнем. Такой комплект защитит от дождя, снега и ветра вашего питомца, а также сбережёт от механических повреждений саму переноску.", Discount=10, UserId="Admin"},
               new Product {ProductId=17, ProductName="КерамикАрт блюдце керамическое", CategoryId = 2, Price = 362.99, Image="cats/c11.jpg", Condition="Милая керамическая миска-блюдце в форме мордочки кошки.", Discount=10, UserId="Admin"},
               new Product {ProductId=18, ProductName="Kitty City гильотина-когтерез", CategoryId = 2, Price = 399.99, Image="cats/c12.jpg", Condition="Когтерез-гильотина из нержавеющей стали для кошек и собак малых и средних пород.", Discount=10, UserId="Admin"},
               
               new Product {ProductId=19, ProductName="Yami-Yami колесо для грызунов", CategoryId = 3, Price = 599.99, Image="others/o1.jpg", Condition="Колесо беговое на подставке для грызунов. Диаметр 25 см,основание - металлическая сетка.", Discount=10, UserId="Admin"},
               new Product {ProductId=20, ProductName="Yami-Yami сено луговое", CategoryId = 3, Price = 219.99, Image="others/o2.jpg", Condition="Сено луговое для кроликов, хорьков, грызунов и пресмыкающихся.", Discount=10, UserId="Admin"},
               new Product {ProductId=21, ProductName="Dr.Alex корм для волнистых попугаев", CategoryId = 3, Price = 156.99, Image="others/o3.jpg", Condition="Серия профилактических кормов для попугаев Dr. ALEX - это уникальное сочетание отборной зерновой смеси экологически чистых зерновых культур и высокоэффективных витаминно-минеральных комплексов. В зависимости от вида наши корма помогут пернатому питомцу укрепить иммунитет, нормализовать обмен веществ, сделают окрас пера ярким, блестящим и просто помогут оставаться здоровым.", Discount=10, UserId="Admin"},
               new Product {ProductId=22, ProductName="Tetra внутренний фильтр FilterJet 400", CategoryId = 3, Price = 1449.99, Image="others/o4.jpg", Condition="Тетра ФильтрДжет для аквариумов объемом 50–120 литров идеально очищает воду благодаря комплексной фильтрации: механической и биологической. Устройство устраняет как крупные, так и небольшие частицы грязи в аквариумной воде и создает биологический баланс, необходимый для здоровой среды в аквариуме.", Discount=10, UserId="Admin"},
            };

            return products;
        }
    }
}
