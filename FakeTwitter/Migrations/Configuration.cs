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
                var appUserAux = new ApplicationUser("robert")
                {
                    Email = "roberto@carlosenterprises.com",
                    EmailConfirmed = true
                };

                context.SaveChanges();

                var robertoUser = context.Users.Add(appUserAux);

                // context.SaveChanges();

                robertoUser.Roles.Add(new IdentityUserRole {
                    RoleId = userRoleId
                });

                // context.SaveChanges();

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

                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

                //                                          //PASOS PARA CREAR UN USUARIO:
                //                                          //      1.- Se crea un Application User
                //                                          //      2.- Se le añade el rol deseado
                //                                          //      3.- Se hace el store.SetPasswordHashAsync con
                //                                          //          la variable del ApplicationUser
                //                                          //      4.- Se hace el context.People.AddOrUpdate pasando
                //                                          //          la relación Ej.
                //                                          //          ApplicationUser = robertoUser
                //                                          //      5.- context.SaveChanges();
                //                                          //Ejemplo mostrado a continuación

                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

                //                                          //      1.- Se crea un Application User
                ApplicationUser rubenUser = context.Users.Add(new ApplicationUser("ruben")
                {
                    Email = "ruben@carlosenterprises.com",
                    EmailConfirmed = true
                });

                //                                          //      2.- Se le añade el rol deseado
                rubenUser.Roles.Add(new IdentityUserRole { RoleId = userRoleId });

                //                                          //      3.- Se hace el store.SetPasswordHashAsync con
                //                                          //          la variable del ApplicationUser
                store.SetPasswordHashAsync(
                    rubenUser,
                    new TwitterUserManager().PasswordHasher.HashPassword("ruben123")
                );

                //                                          //      4.- Se hace el context.People.AddOrUpdate pasando
                //                                          //          la relación
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

                //                                          //      5.- context.SaveChanges();
                context.SaveChanges();
                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                ApplicationUser juanjoUser = context.Users.Add(new ApplicationUser("juanjo")
                {
                    Email = "juanjo@carlosenterprises.com",
                    EmailConfirmed = true
                });

                juanjoUser.Roles.Add(new IdentityUserRole { RoleId = userRoleId });

                store.SetPasswordHashAsync(
                    juanjoUser,
                    new TwitterUserManager().PasswordHasher.HashPassword("juanjo123")
                );

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
                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                ApplicationUser angelnavilUser = context.Users.Add(new ApplicationUser("angelnavil")
                {
                    Email = "angelnavil@carlosenterprises.com",
                    EmailConfirmed = true
                });

                angelnavilUser.Roles.Add(new IdentityUserRole { RoleId = userRoleId });

                store.SetPasswordHashAsync(
                    angelnavilUser,
                    new TwitterUserManager().PasswordHasher.HashPassword("angelnavil123")
                );

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
                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                ApplicationUser paquitoUser = context.Users.Add(new ApplicationUser("paquito")
                {
                    Email = "paquito@carlosenterprises.com",
                    EmailConfirmed = true
                });

                paquitoUser.Roles.Add(new IdentityUserRole { RoleId = userRoleId });
                
                store.SetPasswordHashAsync(
                    paquitoUser,
                    new TwitterUserManager().PasswordHasher.HashPassword("paquito123")
                );

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
                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                ApplicationUser littleCesarUser = context.Users.Add(new ApplicationUser("littleCesar")
                {
                    Email = "littleCesar@carlosenterprises.com",
                    EmailConfirmed = true
                });

                littleCesarUser.Roles.Add(new IdentityUserRole { RoleId = userRoleId });

                store.SetPasswordHashAsync(
                    littleCesarUser,
                    new TwitterUserManager().PasswordHasher.HashPassword("littleCesar123")
                );

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
                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                ApplicationUser chiuntiUser = context.Users.Add(new ApplicationUser("chiunti")
                {
                    Email = "leoncio@carlosenterprises.com",
                    EmailConfirmed = true
                });

                chiuntiUser.Roles.Add(new IdentityUserRole { RoleId = userRoleId });

                store.SetPasswordHashAsync(
                    chiuntiUser,
                    new TwitterUserManager().PasswordHasher.HashPassword("chiunti123")
                );

                context.People.AddOrUpdate(new Person
                {
                    Id = 7,
                    Name = "Leoncio Chiunti",
                    Email = "leoncio@carlosenterprises.com",
                    At = "chiunti",
                    GroupId = 2,
                    Photo = "https://pbs.twimg.com/media/CXfFIEuWQAACkR7.png:large",
                    ApplicationUser = chiuntiUser
                });

                context.SaveChanges();
                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                ApplicationUser Immutare_User = context.Users.Add(new ApplicationUser("Immutare_")
                {
                    Email = "carlos@carlosenterprises.com",
                    EmailConfirmed = true
                });

                Immutare_User.Roles.Add(new IdentityUserRole { RoleId = userRoleId });
                
                store.SetPasswordHashAsync(
                    Immutare_User,
                    new TwitterUserManager().PasswordHasher.HashPassword("Immutare_123")
                );

                context.People.AddOrUpdate(new Person
                {
                    Id = 8,
                    Name = "Carlos GG",
                    Email = "carlos@carlosenterprises.com",
                    At = "Immutare_",
                    GroupId = 3,
                    Photo = "https://pbs.twimg.com/profile_images/986458652205821952/kl3dYcRa_400x400.jpg",
                    ApplicationUser = Immutare_User
                });

                context.SaveChanges();
                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                ApplicationUser AngelUser = context.Users.Add(new ApplicationUser("AngelT")
                {
                    Email = "angeltr@carlosenterprises.com",
                    EmailConfirmed = true
                });

                AngelUser.Roles.Add(new IdentityUserRole { RoleId = userRoleId });

                store.SetPasswordHashAsync(
                    AngelUser,
                    new TwitterUserManager().PasswordHasher.HashPassword("AngelT123")
                );

                context.People.AddOrUpdate(new Person
                {
                    Id = 9,
                    Name = "AngelT",
                    Email = "angeltr@carlosenterprises.com",
                    At = "AngelT",
                    GroupId = 3,
                    Photo = "https://pbs.twimg.com/media/CXfFIEuWQAACkR7.png:large",
                    ApplicationUser = AngelUser
                });

                context.SaveChanges();
                //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                ApplicationUser catyhdzUser = context.Users.Add(new ApplicationUser("catyhdz")
                {
                    Email = "catyhdz@carlosenterprises.com",
                    EmailConfirmed = true
                });
                catyhdzUser.Roles.Add(new IdentityUserRole { RoleId = userRoleId });

                store.SetPasswordHashAsync(
                    catyhdzUser, new TwitterUserManager().PasswordHasher.HashPassword("catyhdz123")
                );

                context.People.AddOrUpdate(new Person
                {
                    Id = 10,
                    Name = "Cath",
                    Email = "catherine@carlosenterprises.com",
                    At = "catyhdz",
                    GroupId = 3,
                    Photo = "https://pbs.twimg.com/media/CXfFIEuWQAACkR7.png:large",
                    ApplicationUser = catyhdzUser
                });

                context.SaveChanges();
            }

            // context.SaveChanges();

            
            //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            //                                              //Tweets
            //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            context.Tweets.AddOrUpdate(new Tweet { Id = 1, DatePublished = System.DateTime.Now, Images = null, PersonId = 1, Text = "Hello World!" });
            context.Tweets.AddOrUpdate(new Tweet { Id = 2, DatePublished = System.DateTime.Now, Images = null, PersonId = 2, Text = "Hello World!" });
            context.Tweets.AddOrUpdate(new Tweet { Id = 3, DatePublished = System.DateTime.Now, Images = null, PersonId = 3, Text = "Hello World!" });
            context.Tweets.AddOrUpdate(new Tweet { Id = 4, DatePublished = System.DateTime.Now, Images = null, PersonId = 4, Text = "Hello World!" });
            context.Tweets.AddOrUpdate(new Tweet { Id = 5, DatePublished = System.DateTime.Now, Images = null, PersonId = 5, Text = "Hello World!" });
            context.Tweets.AddOrUpdate(new Tweet { Id = 6, DatePublished = System.DateTime.Now, Images = null, PersonId = 6, Text = "Hello World!" });
            context.Tweets.AddOrUpdate(new Tweet { Id = 7, DatePublished = System.DateTime.Now, Images = null, PersonId = 7, Text = "Hello World!" });
            context.Tweets.AddOrUpdate(new Tweet { Id = 8, DatePublished = System.DateTime.Now, Images = null, PersonId = 8, Text = "Hello World!" });
            context.Tweets.AddOrUpdate(new Tweet { Id = 9, DatePublished = System.DateTime.Now, Images = null, PersonId = 9, Text = "Hello World!" });
            context.Tweets.AddOrUpdate(new Tweet { Id = 10, DatePublished = System.DateTime.Now, Images = null, PersonId = 10, Text = "Hello World!" });

            context.SaveChanges();

            context.Tweets.AddOrUpdate(new Tweet { Id = 10, DatePublished = System.DateTime.Now, Images = null, PersonId = 10, Text = "I came early to work!" });
            context.Tweets.AddOrUpdate(new Tweet { Id = 11, DatePublished = System.DateTime.Now, Images = null, PersonId = 9, Text = "No way", ResponseId = 10});
            context.Tweets.AddOrUpdate(new Tweet { Id = 12, DatePublished = System.DateTime.Now, Images = null, PersonId = 8, Text = "Is it April Fools?", ResponseId = 10 });
            context.Tweets.AddOrUpdate(new Tweet { Id = 13, DatePublished = System.DateTime.Now, Images = null, PersonId = 9, Text = "I know, right?", ResponseId = 12});

            context.SaveChanges();

            context.Tweets.AddOrUpdate(new Tweet { Id = 14, DatePublished = System.DateTime.Now, Images = null, PersonId = 3, Text = "Ready to work!"});
            context.Tweets.AddOrUpdate(new Tweet { Id = 15, DatePublished = System.DateTime.Now, Images = null, PersonId = 4, Text = "Who's up for some Carl's Jr?"});
            context.Tweets.AddOrUpdate(new Tweet { Id = 16, DatePublished = System.DateTime.Now, Images = null, PersonId = 5, Text = "Swift is the best language!"});
            context.Tweets.AddOrUpdate(new Tweet { Id = 17, DatePublished = System.DateTime.Now, Images = null, PersonId = 6, Text = "Androids gives so much trouble"});
            context.Tweets.AddOrUpdate(new Tweet { Id = 18, DatePublished = System.DateTime.Now, Images = null, PersonId = 7, Text = "Coffee makes me happy"});
            context.Tweets.AddOrUpdate(new Tweet { Id = 19, DatePublished = System.DateTime.Now, Images = null, PersonId = 3, Text = "I'm going to have a meeting at lunch :(", ResponseId = 15});

            context.SaveChanges();

            context.Tweets.AddOrUpdate(new Tweet { Id = 20, DatePublished = System.DateTime.Now, Images = null, PersonId = 1, Text = "Meeting at lunch?!"});
            context.Tweets.AddOrUpdate(new Tweet { Id = 21, DatePublished = System.DateTime.Now, Images = null, PersonId = 2, Text = "Again?! Uhg, at least I brought lunch to work", ResponseId = 20});
            context.Tweets.AddOrUpdate(new Tweet { Id = 22, DatePublished = System.DateTime.Now, Images = null, PersonId = 1, Text = "I did not :(", ResponseId = 21});
            context.Tweets.AddOrUpdate(new Tweet { Id = 23, DatePublished = System.DateTime.Now, Images = null, PersonId = 2, Text = "Smoke break!"});
            context.Tweets.AddOrUpdate(new Tweet { Id = 24, DatePublished = System.DateTime.Now, Images = null, PersonId = 1, Text = "Those markers dry quick as a cricket"});

            context.SaveChanges();

            context.Tweets.AddOrUpdate(new Tweet { Id = 25, DatePublished = System.DateTime.Now, Images = null, PersonId = 8, Text = "Backend is up and running!"});
            context.Tweets.AddOrUpdate(new Tweet { Id = 26, DatePublished = System.DateTime.Now, Images = null, PersonId = 9, Text = "I finally understood Angular! Woo!"});
            context.Tweets.AddOrUpdate(new Tweet { Id = 27, DatePublished = System.DateTime.Now, Images = null, PersonId = 8, Text = "Slowpoke", ResponseId = 26});

            context.SaveChanges();
        }
    }
}
