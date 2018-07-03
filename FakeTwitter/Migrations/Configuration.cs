namespace FakeTwitter.Migrations
{
    using FakeTwitter.Core;
    using FakeTwitter.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class Configuration : DbMigrationsConfiguration<FakeTwitterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FakeTwitterContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            //                                              //Groups
            //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            context.Groups.AddOrUpdate(new Group { Id = 1, Name = "Analistas" });
            context.Groups.AddOrUpdate(new Group { Id = 2, Name = "Towa" });
            context.Groups.AddOrUpdate(new Group { Id = 3, Name = "Mesa 1" });
            //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            //                                              //Roles
            //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // context
            string adminRoleId;
            string userRoleId;
            if (!context.Roles.Any())
            {
                adminRoleId = context.Roles.Add(new IdentityRole("Administrator")).Id;
                userRoleId = context.Roles.Add(new IdentityRole("User")).Id;
            }
            else
            {
                adminRoleId = context.Roles.First(c => c.Name == "Administrator").Id;
                userRoleId = context.Roles.First(c => c.Name == "User").Id;
            }

            //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            //                                              //ApplicationUsers
            //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            /*var standardUser = context.Users.Add(new ApplicationUser("robert")
            {
                Email = "",
                EmailConfirmed = true                   //NOTE: Falta validar la confirmación del correo
            });
            standardUser.Roles.Add(new IdentityUserRole { RoleId = userRoleId });

            context.SaveChanges();
            */
            //ApplicationUser administrator;

            if (!context.Users.Any())
            {
                /*
                administrator = context.Users.Add(new ApplicationUser("administrator")
                {
                    Email = "admin@carlosenterprises.com",
                    EmailConfirmed = true
                });
                administrator.Roles.Add(new IdentityUserRole { RoleId = adminRoleId });
                */
                var store = new TwitterUserStore();

                var robertoUser = context.Users.Add(new ApplicationUser("robert")
                {
                    Email = "roberto@carlosenterprises.com",
                    EmailConfirmed = true
                });
                robertoUser.Roles.Add(new IdentityUserRole { RoleId = userRoleId });

                context.SaveChanges();

                // context.SaveChanges();
                /*
                store.SetPasswordHashAsync(
                    administrator,
                    new TwitterUserManager().PasswordHasher.HashPassword("administrator123")
                );
                */
                store.SetPasswordHashAsync(
                    robertoUser,
                    new TwitterUserManager().PasswordHasher.HashPassword("robert123")
                );
                context.SaveChanges();

                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                ApplicationUser rubenUser = context.Users.Add(new ApplicationUser("ruben")
                {
                    Email = "ruben@carlosenterprises.com",
                    EmailConfirmed = true
                });
                rubenUser.Roles.Add(new IdentityUserRole { RoleId = userRoleId });
                context.SaveChanges();
                store.SetPasswordHashAsync(
                    rubenUser,
                    new TwitterUserManager().PasswordHasher.HashPassword("ruben123")
                );
                context.SaveChanges();
                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                ApplicationUser juanjoUser = context.Users.Add(new ApplicationUser("juanjo")
                {
                    Email = "juanjo@carlosenterprises.com",
                    EmailConfirmed = true
                });
                juanjoUser.Roles.Add(new IdentityUserRole { RoleId = userRoleId });

                context.SaveChanges();

                store.SetPasswordHashAsync(
                    juanjoUser,
                    new TwitterUserManager().PasswordHasher.HashPassword("juanjo123")
                );
                context.SaveChanges();
                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                ApplicationUser angelnavilUser = context.Users.Add(new ApplicationUser("angelnavil")
                {
                    Email = "angelnavil@carlosenterprises.com",
                    EmailConfirmed = true
                });
                angelnavilUser.Roles.Add(new IdentityUserRole { RoleId = userRoleId });

                context.SaveChanges();

                store.SetPasswordHashAsync(
                    angelnavilUser,
                    new TwitterUserManager().PasswordHasher.HashPassword("angelnavil123")
                );

                context.SaveChanges();
                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                ApplicationUser paquitoUser = context.Users.Add(new ApplicationUser("paquito")
                {
                    Email = "paquito@carlosenterprises.com",
                    EmailConfirmed = true
                });
                paquitoUser.Roles.Add(new IdentityUserRole { RoleId = userRoleId });

                context.SaveChanges();

                store.SetPasswordHashAsync(
                    paquitoUser,
                    new TwitterUserManager().PasswordHasher.HashPassword("paquito123")
                );
                context.SaveChanges();
                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                ApplicationUser littleCesarUser = context.Users.Add(new ApplicationUser("littleCesar")
                {
                    Email = "littleCesar@carlosenterprises.com",
                    EmailConfirmed = true
                });
                littleCesarUser.Roles.Add(new IdentityUserRole { RoleId = userRoleId });

                context.SaveChanges();

                store.SetPasswordHashAsync(
                    littleCesarUser,
                    new TwitterUserManager().PasswordHasher.HashPassword("littleCesar123")
                );
                context.SaveChanges();
                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                ApplicationUser chiuntiUser = context.Users.Add(new ApplicationUser("chiunti")
                {
                    Email = "leoncio@carlosenterprises.com",
                    EmailConfirmed = true
                });
                chiuntiUser.Roles.Add(new IdentityUserRole { RoleId = userRoleId });

                context.SaveChanges();

                store.SetPasswordHashAsync(
                    chiuntiUser,
                    new TwitterUserManager().PasswordHasher.HashPassword("chiunti123")
                );
                context.SaveChanges();
                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                ApplicationUser Immutare_User = context.Users.Add(new ApplicationUser("Immutare_")
                {
                    Email = "carlos@carlosenterprises.com",
                    EmailConfirmed = true
                });
                Immutare_User.Roles.Add(new IdentityUserRole { RoleId = userRoleId });

                context.SaveChanges();

                store.SetPasswordHashAsync(
                    Immutare_User,
                    new TwitterUserManager().PasswordHasher.HashPassword("Immutare_123")
                );
                context.SaveChanges();
                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                ApplicationUser angelTrevinoUser = context.Users.Add(new ApplicationUser("angelTrevino")
                {
                    Email = "angel@carlosenterprises.com",
                    EmailConfirmed = true
                });
                angelTrevinoUser.Roles.Add(new IdentityUserRole { RoleId = userRoleId });

                context.SaveChanges();

                store.SetPasswordHashAsync(
                    angelTrevinoUser,
                    new TwitterUserManager().PasswordHasher.HashPassword("angelTrevino123")
                );

                context.SaveChanges();
                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                ApplicationUser catyhdzUser = context.Users.Add(new ApplicationUser("catyhdz")
                {
                    Email = "catyhdz@carlosenterprises.com",
                    EmailConfirmed = true
                });
                catyhdzUser.Roles.Add(new IdentityUserRole { RoleId = userRoleId });

                context.SaveChanges();

                store.SetPasswordHashAsync(
                    catyhdzUser,
                    new TwitterUserManager().PasswordHasher.HashPassword("catyhdz123")
                );

                context.SaveChanges();
                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                //                                              //People
                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

                context.People.AddOrUpdate(new Person
                {
                    Id = 1,
                    Name = "Roberto",
                    Email = "roberto@carlosenterprises.com",
                    At = "robert",
                    GroupId = 1,
                    Photo = "https://pbs.twimg.com/media/CXfFIEuWQAACkR7.png:large",
                    ApplicationUser = robertoUser
                });

                context.SaveChanges();

                context.People.AddOrUpdate(new Person
                {
                    Id = 2,
                    Name = "Ruben",
                    Email = "ruben@carlosenterprises.com",
                    At = "ruben",
                    GroupId = 1,
                    Photo = "https://pbs.twimg.com/media/CXfFIEuWQAACkR7.png:large",
                    ApplicationUser = rubenUser
                });

                context.SaveChanges();

                context.People.AddOrUpdate(new Person
                {
                    Id = 3,
                    Name = "JuanJo",
                    Email = "juanjo@carlosenterprises.com",
                    At = "juanjo",
                    GroupId = 2,
                    Photo = "https://pbs.twimg.com/media/CXfFIEuWQAACkR7.png:large",
                    ApplicationUser = juanjoUser
                });

                context.SaveChanges();

                context.People.AddOrUpdate(new Person
                {
                    Id = 4,
                    Name = "Navil",
                    Email = "angel@carlosenterprises.com",
                    At = "angelnavil",
                    GroupId = 2,
                    Photo = "https://pbs.twimg.com/media/CXfFIEuWQAACkR7.png:large",
                    ApplicationUser = angelnavilUser
                });

                context.SaveChanges();

                context.People.AddOrUpdate(new Person
                {
                    Id = 5,
                    Name = "Francisco",
                    Email = "francisco@carlosenterprises.com",
                    At = "paquito",
                    GroupId = 2,
                    Photo = "https://pbs.twimg.com/media/CXfFIEuWQAACkR7.png:large",
                    ApplicationUser = paquitoUser
                });

                context.SaveChanges();

                context.People.AddOrUpdate(new Person
                {
                    Id = 6,
                    Name = "Cesar",
                    Email = "cesar@carlosenterprises.com",
                    At = "littleCesar",
                    GroupId = 2,
                    Photo = "https://pbs.twimg.com/media/CXfFIEuWQAACkR7.png:large",
                    ApplicationUser = littleCesarUser
                });

                context.SaveChanges();

                context.People.AddOrUpdate(new Person
                {
                    Id = 6,
                    Name = "Leoncio Chiunti",
                    Email = "leoncio@carlosenterprises.com",
                    At = "chiunti",
                    GroupId = 2,
                    Photo = "https://pbs.twimg.com/media/CXfFIEuWQAACkR7.png:large"
                });

                context.SaveChanges();

                context.People.AddOrUpdate(new Person
                {
                    Id = 7,
                    Name = "Carlos GG",
                    Email = "carlos@carlosenterprises.com",
                    At = "Immutare_",
                    GroupId = 3,
                    Photo = "https://pbs.twimg.com/profile_images/986458652205821952/kl3dYcRa_400x400.jpg",
                    ApplicationUser = Immutare_User
                });

                context.SaveChanges();

                context.People.AddOrUpdate(new Person
                {
                    Id = 8,
                    Name = "Angel",
                    Email = "angel@carlosenterprises.com",
                    At = "angelTrevino",
                    GroupId = 3,
                    Photo = "https://pbs.twimg.com/media/CXfFIEuWQAACkR7.png:large",
                    ApplicationUser = angelTrevinoUser
                });

                context.SaveChanges();

                context.People.AddOrUpdate(new Person
                {
                    Id = 9,
                    Name = "Cath",
                    Email = "catherine@carlosenterprises.com",
                    At = "catyhdz",
                    GroupId = 3,
                    Photo = "https://pbs.twimg.com/media/CXfFIEuWQAACkR7.png:large",
                    ApplicationUser = catyhdzUser
                });

                context.SaveChanges();
            }

            context.SaveChanges();

            
            //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            //                                              //Tweets
            //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            context.Tweets.AddOrUpdate(new Tweet { Id = 1, DatePublished = System.DateTime.Now, Images = null, PersonId = 1, Text = "Hello World!", });
        }
    }
}
