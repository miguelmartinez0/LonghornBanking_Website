using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team15_FinalProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;


namespace Team15_FinalProject.Migrations
{
    public class SeedIdentity
    {
        public static void SeedUsers(AppDbContext db)
        {
            //create a user manager to add users to databases
            UserManager<AppUser> userManager = new UserManager<AppUser>(new UserStore<AppUser>(db));
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            //create a role manager
            AppRoleManager roleManager = new AppRoleManager(new RoleStore<AppRole>(db));

            //create a customer role
            String roleName = "Customer";
            //check to see if the role exists
            if (roleManager.RoleExists(roleName) == false) //role doesn't exist
            {
                roleManager.Create(new AppRole(roleName));
            }

            //create a user
            String strEmail1 = "cbaker@freezing.co.uk";
            var c1 = new AppUser()
            {
                UserName = strEmail1,
                Email = strEmail1,
                FName = "Christopher",
                MInitial = "L",
                LName = "Baker",
                StreetAddress = "1245 Lake Austin Blvd.",
                City = "Austin",
                State = States.TX,
                Zip = 78733,
                Phone = "(512)557-1146",
                Birthday = DateTime.Parse("02/07/1991")
            };
            //see if the user is already there
            AppUser c1ToAdd = db.Users.Where(c => c.Email == strEmail1).FirstOrDefault();
            if (c1ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c1, "gazing");
                c1ToAdd = db.Users.Single(c => c.Email == strEmail1);

                //add the user to the role
                if (userManager.IsInRole(c1ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c1ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail2 = "mb@aool.com";
            var c2 = new AppUser()
            {
                UserName = strEmail2,
                Email = strEmail2,
                FName = "Michelle",
                LName = "Banks",
                StreetAddress = "1300 Tall Pine Lane",
                City = "San Antonio",
                State = States.TX,
                Zip = 78261,
                Phone = "(210)267-8873",
                Birthday = DateTime.Parse("6/23/1990")
            };
            //see if the user is already there
            AppUser c2ToAdd = db.Users.Where(c => c.Email == strEmail2).FirstOrDefault();
            if (c2ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c2, "banquet");
                c2ToAdd = db.Users.Single(c => c.Email == strEmail2);

                //add the user to the role
                if (userManager.IsInRole(c2ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c2ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail3 = "fd@aool.com";
            var c3 = new AppUser()
            {
                UserName = strEmail3,
                Email = "fd@aool.com",
                FName = "Franco",
                MInitial = "V",
                LName = "Broccolo",
                StreetAddress = "62 Browning Rd",
                City = "Houston",
                State = States.TX,
                Zip = 77019,
                Phone = "(817)565-9699",
                Birthday = DateTime.Parse("05/06/1986")
            };

            //see if the user is already there
            AppUser c3ToAdd = db.Users.Where(c => c.Email == strEmail3).FirstOrDefault();
            if (c3ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c3, "666666");
                c3ToAdd = db.Users.Single(c => c.Email == strEmail3);

                //add the user to the role
                if (userManager.IsInRole(c3ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c3ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail4 = "wendy@ggmail.com";
            var c4 = new AppUser()
            {
                UserName = strEmail4,
                Email = strEmail4,
                FName = "Wendy",
                MInitial = "L",
                LName = "Chang",
                StreetAddress = "202 Bellmont Hall",
                City = "Austin",
                State = States.TX,
                Zip = 78713,
                Phone = "(512)594-3222",
                Birthday = DateTime.Parse("12/21/1964")
            };

            //see if the user is already there
            AppUser c4ToAdd = db.Users.Where(c => c.Email == strEmail4).FirstOrDefault();
            if (c4ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c4, "clover");
                c4ToAdd = db.Users.Single(c => c.Email == strEmail4);

                //add the user to the role
                if (userManager.IsInRole(c4ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c4ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail5 = "limchou@yaho.com";
            var c5 = new AppUser()
            {
                UserName = strEmail5,
                Email = strEmail5,
                FName = "Lim",
                LName = "Chou",
                StreetAddress = "1600 Teresa Lane",
                City = "San Antonio",
                State = States.TX,
                Zip = 78266,
                Phone = "(210)772-4599",
                Birthday = DateTime.Parse("6/14/1950")
            };
            //see if the user is already there
            AppUser c5ToAdd = db.Users.Where(c => c.Email == strEmail5).FirstOrDefault();
            if (c5ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c5, "austin");
                c5ToAdd = db.Users.Single(c => c.Email == strEmail5);

                //add the user to the role
                if (userManager.IsInRole(c5ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c5ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail6 = "Dixon@aool.com";
            var c6 = new AppUser()
            {
                UserName = strEmail6,
                Email = strEmail6,
                FName = "Shan",
                MInitial = "D",
                LName = "Dixon",
                StreetAddress = "234 Holston Circle",
                City = "Dallas",
                State = States.TX,
                Zip = 75208,
                Phone = "(214)264-3255",
                Birthday = DateTime.Parse("5/9/1930")
            };
            //see if the user is already there
            AppUser c6ToAdd = db.Users.Where(c => c.Email == strEmail6).FirstOrDefault();
            if (c6ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c6, "mailbox");
                c6ToAdd = db.Users.Single(c => c.Email == strEmail6);

                //add the user to the role
                if (userManager.IsInRole(c6ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c6ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail7 = "louann@ggmail.com";
            var c7 = new AppUser()
            {
                UserName = strEmail7,
                Email = strEmail7,
                FName = "Lou Ann",
                MInitial = "K",
                LName = "Feeley",
                StreetAddress = "600 S 8th Street W",
                City = "Houston",
                State = States.TX,
                Zip = 77010,
                Phone = "(817)255-6749",
                Birthday = DateTime.Parse("2/24/1930")
            };
            //see if the user is already there
            AppUser c7ToAdd = db.Users.Where(c => c.Email == strEmail7).FirstOrDefault();
            if (c7ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c7, "aggies");
                c7ToAdd = db.Users.Single(c => c.Email == strEmail7);

                //add the user to the role
                if (userManager.IsInRole(c7ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c7ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail8 = "tfreeley@minntonka.ci.state.mn.us";
            var c8 = new AppUser()
            {
                UserName = strEmail8,
                Email = strEmail8,
                FName = "Tesa",
                MInitial = "P",
                LName = "Freeley",
                StreetAddress = "4448 Fairview Ave.",
                City = "Houston",
                State = States.TX,
                Zip = 77009,
                Phone = "(817)325-5687",
                Birthday = DateTime.Parse("9/1/1935")
            };
            //see if the user is already there
            AppUser c8ToAdd = db.Users.Where(c => c.Email == strEmail8).FirstOrDefault();
            if (c8ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c8, "raiders");
                c8ToAdd = db.Users.Single(c => c.Email == strEmail8);

                //add the user to the role
                if (userManager.IsInRole(c8ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c8ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail9 = "mgar@aool.com";
            var c9 = new AppUser()
            {
                UserName = strEmail9,
                Email = strEmail9,
                FName = "Margaret",
                MInitial = "L",
                LName = "Garcia",
                StreetAddress = "594 Longview",
                City = "Houston",
                State = States.TX,
                Zip = 77003,
                Phone = "(817)659-3544",
                Birthday = DateTime.Parse("7/3/1990")
            };
            //see if the user is already there
            AppUser c9ToAdd = db.Users.Where(c => c.Email == strEmail9).FirstOrDefault();
            if (c9ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c9, "mustangs");
                c9ToAdd = db.Users.Single(c => c.Email == strEmail9);

                //add the user to the role
                if (userManager.IsInRole(c9ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c9ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail10 = "chaley@thug.com";
            var c10 = new AppUser()
            {
                UserName = strEmail10,
                Email = strEmail10,
                FName = "Charles",
                MInitial = "E",
                LName = "Haley",
                StreetAddress = "One Cowboy Pkwy",
                City = "Dallas",
                State = States.TX,
                Zip = 75261,
                Phone = "(214)847-5583",
                Birthday = DateTime.Parse("9/17/1985")
            };
            //see if the user is already there
            AppUser c10ToAdd = db.Users.Where(c => c.Email == strEmail10).FirstOrDefault();
            if (c10ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c10, "region");
                c10ToAdd = db.Users.Single(c => c.Email == strEmail10);

                //add the user to the role
                if (userManager.IsInRole(c10ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c10ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail11 = "jeff@ggmail.com";
            var c11 = new AppUser()
            {
                UserName = strEmail11,
                Email = strEmail11,
                FName = "Jeffrey",
                MInitial = "T",
                LName = "Hampton",
                StreetAddress = "337 38th St.",
                City = "Austin",
                State = States.TX,
                Zip = 78705,
                Phone = "(512)697-8613",
                Birthday = DateTime.Parse("1/23/1995")
            };
            //see if the user is already there
            AppUser c11ToAdd = db.Users.Where(c => c.Email == strEmail11).FirstOrDefault();
            if (c11ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c11, "hungry");
                c11ToAdd = db.Users.Single(c => c.Email == strEmail11);

                //add the user to the role
                if (userManager.IsInRole(c11ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c11ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail12 = "wjhearniii@umch.edu";
            var c12 = new AppUser()
            {
                UserName = strEmail12,
                Email = strEmail12,
                FName = "John",
                MInitial = "B",
                LName = "Hearn",
                StreetAddress = "4225 North First",
                City = "Dallas",
                State = States.TX,
                Zip = 75237,
                Phone = "(214)896-5621",
                Birthday = DateTime.Parse("1/8/1994")
            };
            //see if the user is already there
            AppUser c12ToAdd = db.Users.Where(c => c.Email == strEmail12).FirstOrDefault();
            if (c12ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c12, "logicon");
                c12ToAdd = db.Users.Single(c => c.Email == strEmail12);

                //add the user to the role
                if (userManager.IsInRole(c12ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c12ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail13 = "hicks43@ggmail.com";
            var c13 = new AppUser()
            {
                UserName = strEmail13,
                Email = strEmail13,
                FName = "Anthony",
                MInitial = "J",
                LName = "Hicks",
                StreetAddress = "32 NE Garden Ln., Ste 910",
                City = "San Antonio",
                State = States.TX,
                Zip = 78239,
                Phone = "(210)578-8965",
                Birthday = DateTime.Parse("10/6/1990")
            };
            //see if the user is already there
            AppUser c13ToAdd = db.Users.Where(c => c.Email == strEmail13).FirstOrDefault();
            if (c13ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c13, "doofus");
                c13ToAdd = db.Users.Single(c => c.Email == strEmail13);

                //add the user to the role
                if (userManager.IsInRole(c13ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c13ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail14 = "bradsingram@mall.utexas.edu";
            var c14 = new AppUser()
            {
                UserName = strEmail14,
                Email = strEmail14,
                FName = "Brad",
                MInitial = "S",
                LName = "Ingram",
                StreetAddress = "6548 La Posada Ct.",
                City = "Austin",
                State = States.TX,
                Zip = 78736,
                Phone = "(512)467-8821",
                Birthday = DateTime.Parse("4/12/1984")
            };
            //see if the user is already there
            AppUser c14ToAdd = db.Users.Where(c => c.Email == strEmail14).FirstOrDefault();
            if (c14ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c14, "mother");
                c14ToAdd = db.Users.Single(c => c.Email == strEmail14);

                //add the user to the role
                if (userManager.IsInRole(c14ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c14ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail15 = "mother.Ingram@aool.com";
            var c15 = new AppUser()
            {
                UserName = strEmail15,
                Email = strEmail15,
                FName = "Todd",
                MInitial = "L",
                LName = "Jacobs",
                StreetAddress = "4564 Elm St.",
                City = "Austin",
                State = States.TX,
                Zip = 78731,
                Phone = "(512)465-3365",
                Birthday = DateTime.Parse("4/4/1983")
            };
            //see if the user is already there
            AppUser c15ToAdd = db.Users.Where(c => c.Email == strEmail15).FirstOrDefault();
            if (c15ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c15, "whimsical");
                c15ToAdd = db.Users.Single(c => c.Email == strEmail15);

                //add the user to the role
                if (userManager.IsInRole(c15ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c15ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail16 = "victoria@aool.com";
            var c16 = new AppUser()
            {
                UserName = strEmail16,
                Email = strEmail16,
                FName = "Victoria",
                MInitial = "M",
                LName = "Lawrence",
                StreetAddress = "6639 Butterfly Ln.",
                City = "Austin",
                State = States.TX,
                Zip = 78761,
                Phone = "(512)945-7399",
                Birthday = DateTime.Parse("2/3/1961")
            };
            //see if the user is already there
            AppUser c16ToAdd = db.Users.Where(c => c.Email == strEmail16).FirstOrDefault();
            if (c16ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c16, "nothing");
                c16ToAdd = db.Users.Single(c => c.Email == strEmail16);

                //add the user to the role
                if (userManager.IsInRole(c16ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c16ToAdd.Id, roleName);
                }
            }


            //create a user
            String strEmail17 = "lineback@flush.net";
            var c17 = new AppUser()
            {
                UserName = strEmail17,
                Email = strEmail17,
                FName = "Erik",
                MInitial = "W",
                LName = "Lineback",
                StreetAddress = "1300 Netherland St",
                City = "San Antonio",
                State = States.TX,
                Zip = 78293,
                Phone = "(210)244-9976",
                Birthday = DateTime.Parse("9/3/1946")
            };
            //see if the user is already there
            AppUser c17ToAdd = db.Users.Where(c => c.Email == strEmail17).FirstOrDefault();
            if (c17ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c17, "GoodFellow");
                c17ToAdd = db.Users.Single(c => c.Email == strEmail17);

                //add the user to the role
                if (userManager.IsInRole(c17ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c17ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail18 = "elowe@netscrape.net";
            var c18 = new AppUser()
            {
                UserName = strEmail18,
                Email = strEmail18,
                FName = "Ernest",
                MInitial = "S",
                LName = "Lowe",
                StreetAddress = "3201 Pine Drive",
                City = "San Antonio",
                State = States.TX,
                Zip = 78279,
                Phone = "(210)534-4627",
                Birthday = DateTime.Parse("2/7/1992")
            };
            //see if the user is already there
            AppUser c18ToAdd = db.Users.Where(c => c.Email == strEmail18).FirstOrDefault();
            if (c18ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c18, "impede");
                c18ToAdd = db.Users.Single(c => c.Email == strEmail18);

                //add the user to the role
                if (userManager.IsInRole(c18ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c18ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail19 = "luce_chuck@ggmail.com";
            var c19 = new AppUser()
            {
                UserName = strEmail19,
                Email = strEmail19,
                FName = "Chuck",
                MInitial = "B",
                LName = "Luce",
                StreetAddress = "2345 Rolling Clouds",
                City = "San Antonio",
                State = States.TX,
                Zip = 78268,
                Phone = "(210)698-3548",
                Birthday = DateTime.Parse("10/25/1942")
            };
            //see if the user is already there
            AppUser c19ToAdd = db.Users.Where(c => c.Email == strEmail19).FirstOrDefault();
            if (c19ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c19, "LuceyDucey");
                c19ToAdd = db.Users.Single(c => c.Email == strEmail19);

                //add the user to the role
                if (userManager.IsInRole(c19ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c19ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail20 = "mackcloud@pimpdaddy.com";
            var c20 = new AppUser()
            {
                UserName = strEmail20,
                Email = strEmail20,
                FName = "Jennifer",
                MInitial = "D",
                LName = "MacLeod",
                StreetAddress = "2504 Far West Blvd.",
                City = "Austin",
                State = States.TX,
                Zip = 78731,
                Phone = "(210)698-3548",
                Birthday = DateTime.Parse("10/25/1942")
            };
            //see if the user is already there
            AppUser c20ToAdd = db.Users.Where(c => c.Email == strEmail20).FirstOrDefault();
            if (c20ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c20, "cloudyday");
                c20ToAdd = db.Users.Single(c => c.Email == strEmail20);

                //add the user to the role
                if (userManager.IsInRole(c20ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c20ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail21 = "liz@ggmail.com";
            var c21 = new AppUser()
            {
                UserName = strEmail21,
                Email = strEmail21,
                FName = "Elizabeth",
                MInitial = "P",
                LName = "Markham",
                StreetAddress = "7861 Chevy Chase",
                City = "Austin",
                State = States.TX,
                Zip = 78732,
                Phone = "(512)457-9845",
                Birthday = DateTime.Parse("4/13/1959")
            };
            //see if the user is already there
            AppUser c21ToAdd = db.Users.Where(c => c.Email == strEmail21).FirstOrDefault();
            if (c21ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c21, "emarkbark");
                c21ToAdd = db.Users.Single(c => c.Email == strEmail21);

                //add the user to the role
                if (userManager.IsInRole(c21ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c21ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail22 = "mclarence@aool.com";
            var c22 = new AppUser()
            {
                UserName = strEmail22,
                Email = strEmail22,
                FName = "Clarence",
                MInitial = "A",
                LName = "Martin",
                StreetAddress = "87 Alcedo St.",
                City = "Houston",
                State = States.TX,
                Zip = 77045,
                Phone = "(817)495-5201",
                Birthday = DateTime.Parse("1/6/1990")
            };
            //see if the user is already there
            AppUser c22ToAdd = db.Users.Where(c => c.Email == strEmail22).FirstOrDefault();
            if (c22ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c22, "smartinmartin");
                c22ToAdd = db.Users.Single(c => c.Email == strEmail22);

                //add the user to the role
                if (userManager.IsInRole(c22ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c22ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail23 = "smartinmartin.Martin@aool.com";
            var c23 = new AppUser()
            {
                UserName = strEmail23,
                Email = strEmail23,
                FName = "Gregory",
                MInitial = "R",
                LName = "Martinez",
                StreetAddress = "8295 Sunset Blvd.",
                City = "Houston",
                State = States.TX,
                Zip = 77030,
                Phone = "(817)874-6718",
                Birthday = DateTime.Parse("10/9/1987")
            };
            //see if the user is already there
            AppUser c23ToAdd = db.Users.Where(c => c.Email == strEmail23).FirstOrDefault();
            if (c23ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c23, "looter");
                c23ToAdd = db.Users.Single(c => c.Email == strEmail23);

                //add the user to the role
                if (userManager.IsInRole(c23ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c23ToAdd.Id, roleName);
                }
            }


            //create a user
            String strEmail24 = "cmiller@mapster.com";
            var c24 = new AppUser()
            {
                UserName = strEmail24,
                Email = strEmail24,
                FName = "Charles",
                MInitial = "R",
                LName = "Miller",
                StreetAddress = "8962 Main St.",
                City = "Houston",
                State = States.TX,
                Zip = 77031,
                Phone = "(817)745-8615",
                Birthday = DateTime.Parse("7/21/1984")
            };
            //see if the user is already there
            AppUser c24ToAdd = db.Users.Where(c => c.Email == strEmail24).FirstOrDefault();
            if (c24ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c24, "chucky33");
                c24ToAdd = db.Users.Single(c => c.Email == strEmail24);

                //add the user to the role
                if (userManager.IsInRole(c24ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c24ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail25 = "nelson.Kelly@aool.com";
            var c25 = new AppUser()
            {
                UserName = strEmail25,
                Email = strEmail25,
                FName = "Kelly",
                MInitial = "T",
                LName = "Nelson",
                StreetAddress = "2601 Red River",
                City = "Austin",
                State = States.TX,
                Zip = 78703,
                Phone = "(512)292-6966",
                Birthday = DateTime.Parse("7/4/1956")
            };
            //see if the user is already there
            AppUser c25ToAdd = db.Users.Where(c => c.Email == strEmail25).FirstOrDefault();
            if (c25ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c25, "orange");
                c25ToAdd = db.Users.Single(c => c.Email == strEmail25);

                //add the user to the role
                if (userManager.IsInRole(c25ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c25ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail26 = "jojoe@ggmail.com";
            var c26 = new AppUser()
            {
                UserName = strEmail26,
                Email = strEmail26,
                FName = "Joe",
                MInitial = "C",
                LName = "Nguyen",
                StreetAddress = "1249 4th SW St.",
                City = "Dallas",
                State = States.TX,
                Zip = 75238,
                Phone = "(214)312-5897",
                Birthday = DateTime.Parse("1/29/1963")
            };
            //see if the user is already there
            AppUser c26ToAdd = db.Users.Where(c => c.Email == strEmail26).FirstOrDefault();
            if (c26ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c26, "victorious");
                c26ToAdd = db.Users.Single(c => c.Email == strEmail26);

                //add the user to the role
                if (userManager.IsInRole(c26ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c26ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail27 = "orielly@foxnets.com";
            var c27 = new AppUser()
            {
                UserName = strEmail27,
                Email = strEmail27,
                FName = "Bill",
                MInitial = "T",
                LName = "O'Reilly",
                StreetAddress = "8800 Gringo Drive",
                City = "San Antonio",
                State = States.TX,
                Zip = 78260,
                Phone = "(210)345-0925",
                Birthday = DateTime.Parse("1/7/1983")
            };
            //see if the user is already there
            AppUser c27ToAdd = db.Users.Where(c => c.Email == strEmail27).FirstOrDefault();
            if (c27ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c27, "billyboy");
                c27ToAdd = db.Users.Single(c => c.Email == strEmail27);

                //add the user to the role
                if (userManager.IsInRole(c27ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c27ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail28 = "or@aool.com";
            var c28 = new AppUser()
            {
                UserName = strEmail28,
                Email = strEmail28,
                FName = "Anka",
                MInitial = "L",
                LName = "Radkovich",
                StreetAddress = "1300 Elliott Pl",
                City = "Dallas",
                State = States.TX,
                Zip = 75260,
                Phone = "(214)234-5566",
                Birthday = DateTime.Parse("3/31/1980")
            };
            //see if the user is already there
            AppUser c28ToAdd = db.Users.Where(c => c.Email == strEmail28).FirstOrDefault();
            if (c28ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c28, "radicalone");
                c28ToAdd = db.Users.Single(c => c.Email == strEmail28);

                //add the user to the role
                if (userManager.IsInRole(c28ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c28ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail29 = "megrhodes@freezing.co.uk";
            var c29 = new AppUser()
            {
                UserName = strEmail29,
                Email = strEmail29,
                FName = "Megan",
                MInitial = "C",
                LName = "Rhodes",
                StreetAddress = "4587 Enfield Rd.",
                City = "Austin",
                State = States.TX,
                Zip = 78707,
                Phone = "(512)374-4746",
                Birthday = DateTime.Parse("8/12/1944")
            };
            //see if the user is already there
            AppUser c29ToAdd = db.Users.Where(c => c.Email == strEmail29).FirstOrDefault();
            if (c29ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c29, "gohorns");
                c29ToAdd = db.Users.Single(c => c.Email == strEmail29);

                //add the user to the role
                if (userManager.IsInRole(c29ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c29ToAdd.Id, roleName);
                }
            }


            //create a user
            String strEmail30 = "erynrice@aool.com";
            var c30 = new AppUser()
            {
                UserName = strEmail30,
                Email = strEmail30,
                FName = "Eryn",
                MInitial = "M",
                LName = "Rice",
                StreetAddress = "3405 Rio Grande",
                City = "Austin",
                State = States.TX,
                Zip = 78705,
                Phone = "(512)387-6657",
                Birthday = DateTime.Parse("8/2/1934")
            };
            //see if the user is already there
            AppUser c30ToAdd = db.Users.Where(c => c.Email == strEmail30).FirstOrDefault();
            if (c30ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c30, "iloveme");
                c30ToAdd = db.Users.Single(c => c.Email == strEmail30);

                //add the user to the role
                if (userManager.IsInRole(c30ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c30ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail31 = "jorge@hootmail.com";
            var c31 = new AppUser()
            {
                UserName = strEmail31,
                Email = strEmail31,
                FName = "Jorge",
                LName = "Rodriguez",
                StreetAddress = "6788 Cotter Street",
                City = "Houston",
                State = States.TX,
                Zip = 77057,
                Phone = "(817)890-4374",
                Birthday = DateTime.Parse("8/11/1989")
            };
            //see if the user is already there
            AppUser c31ToAdd = db.Users.Where(c => c.Email == strEmail31).FirstOrDefault();
            if (c31ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c31, "greedy");
                c31ToAdd = db.Users.Single(c => c.Email == strEmail31);

                //add the user to the role
                if (userManager.IsInRole(c31ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c31ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail32 = "ra@aoo.com";
            var c32 = new AppUser()
            {
                UserName = strEmail32,
                Email = strEmail32,
                FName = "Allen",
                MInitial = "B",
                LName = "Rogers",
                StreetAddress = "4965 Oak Hill",
                City = "Austin",
                State = States.TX,
                Zip = 78732,
                Phone = "(512)875-2943",
                Birthday = DateTime.Parse("8/27/1967")
            };
            //see if the user is already there
            AppUser c32ToAdd = db.Users.Where(c => c.Email == strEmail32).FirstOrDefault();
            if (c32ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c32, "familiar");
                c32ToAdd = db.Users.Single(c => c.Email == strEmail32);

                //add the user to the role
                if (userManager.IsInRole(c32ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c32ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail33 = "stjean@home.com";
            var c33 = new AppUser()
            {
                UserName = strEmail33,
                Email = strEmail33,
                FName = "Olivier",
                MInitial = "M",
                LName = "Saint-Jean",
                StreetAddress = "255 Toncray Dr.",
                City = "San Antonio",
                State = States.TX,
                Zip = 78292,
                Phone = "(210)414-5678",
                Birthday = DateTime.Parse("7/8/1950")
            };
            //see if the user is already there
            AppUser c33ToAdd = db.Users.Where(c => c.Email == strEmail33).FirstOrDefault();
            if (c33ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c33, "historical");
                c33ToAdd = db.Users.Single(c => c.Email == strEmail33);

                //add the user to the role
                if (userManager.IsInRole(c33ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c33ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail34 = "ss34@ggmail.com";
            var c34 = new AppUser()
            {
                UserName = strEmail34,
                Email = strEmail34,
                FName = "Sarah",
                MInitial = "J",
                LName = "Saunders",
                StreetAddress = "332 Avenue C",
                City = "Austin",
                State = States.TX,
                Zip = 78705,
                Phone = "(512)349-7810",
                Birthday = DateTime.Parse("10/29/1977")
            };
            //see if the user is already there
            AppUser c34ToAdd = db.Users.Where(c => c.Email == strEmail34).FirstOrDefault();
            if (c34ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c34, "guiltless");
                c34ToAdd = db.Users.Single(c => c.Email == strEmail34);

                //add the user to the role
                if (userManager.IsInRole(c34ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c34ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail35 = "willsheff@email.com";
            var c35 = new AppUser()
            {
                UserName = strEmail35,
                Email = strEmail35,
                FName = "William",
                MInitial = "T",
                LName = "Sewell",
                StreetAddress = "2365 51st St.",
                City = "Austin",
                State = States.TX,
                Zip = 78709,
                Phone = "(512)451-0084",
                Birthday = DateTime.Parse("4/21/1941")
            };
            //see if the user is already there
            AppUser c35ToAdd = db.Users.Where(c => c.Email == strEmail35).FirstOrDefault();
            if (c35ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c35, "frequent");
                c35ToAdd = db.Users.Single(c => c.Email == strEmail35);

                //add the user to the role
                if (userManager.IsInRole(c35ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c35ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail36 = "sheff44@ggmail.com";
            var c36 = new AppUser()
            {
                UserName = strEmail36,
                Email = strEmail36,
                FName = "Martin",
                MInitial = "J",
                LName = "Sheffield",
                StreetAddress = "3886 Avenue A",
                City = "Austin",
                State = States.TX,
                Zip = 78705,
                Phone = "(512)547-9167",
                Birthday = DateTime.Parse("11/10/1937")
            };
            //see if the user is already there
            AppUser c36ToAdd = db.Users.Where(c => c.Email == strEmail36).FirstOrDefault();
            if (c36ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c36, "history");
                c36ToAdd = db.Users.Single(c => c.Email == strEmail36);

                //add the user to the role
                if (userManager.IsInRole(c36ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c36ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail37 = "johnsmith187@aool.com";
            var c37 = new AppUser()
            {
                UserName = strEmail37,
                Email = strEmail37,
                FName = "John",
                MInitial = "A",
                LName = "Smith",
                StreetAddress = "23 Hidden Forge Dr.",
                City = "San Antonio",
                State = States.TX,
                Zip = 78280,
                Phone = "(210)832-1888",
                Birthday = DateTime.Parse("10/26/1954")
            };
            //see if the user is already there
            AppUser c37ToAdd = db.Users.Where(c => c.Email == strEmail37).FirstOrDefault();
            if (c37ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c37, "squirrel");
                c37ToAdd = db.Users.Single(c => c.Email == strEmail37);

                //add the user to the role
                if (userManager.IsInRole(c37ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c37ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail38 = "dustroud@mail.com";
            var c38 = new AppUser()
            {
                UserName = strEmail38,
                Email = strEmail38,
                FName = "Dustin",
                MInitial = "P",
                LName = "Stroud",
                StreetAddress = "1212 Rita Rd",
                City = "Dallas",
                State = States.TX,
                Zip = 75221,
                Phone = "(214)234-6667",
                Birthday = DateTime.Parse("9/1/1932")
            };
            //see if the user is already there
            AppUser c38ToAdd = db.Users.Where(c => c.Email == strEmail38).FirstOrDefault();
            if (c38ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c38, "snakes");
                c38ToAdd = db.Users.Single(c => c.Email == strEmail38);

                //add the user to the role
                if (userManager.IsInRole(c38ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c38ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail39 = "ericstuart@aool.com";
            var c39 = new AppUser()
            {
                UserName = strEmail39,
                Email = strEmail39,
                FName = "Eric",
                MInitial = "D",
                LName = "Stuart",
                StreetAddress = "5576 Toro Ring",
                City = "Austin",
                State = States.TX,
                Zip = 78746,
                Phone = "(512)817-8335",
                Birthday = DateTime.Parse("12/28/1930")
            };

            //see if the user is already there
            AppUser c39ToAdd = db.Users.Where(c => c.Email == strEmail39).FirstOrDefault();
            if (c39ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c39, "landus");
                c39ToAdd = db.Users.Single(c => c.Email == strEmail39);

                //add the user to the role
                if (userManager.IsInRole(c39ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c39ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail40 = "peterstump@hootmail.com";
            var c40 = new AppUser()
            {
                UserName = strEmail40,
                Email = strEmail40,
                FName = "Peter",
                MInitial = "L",
                LName = "Stump",
                StreetAddress = "1300 Kellen Circle",
                City = "Houston",
                State = States.TX,
                Zip = 78746,
                Phone = "(512)817-8335",
                Birthday = DateTime.Parse("12/28/1930")
            };

            //see if the user is already there
            AppUser c40ToAdd = db.Users.Where(c => c.Email == strEmail40).FirstOrDefault();
            if (c40ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c40, "rhythm");
                c40ToAdd = db.Users.Single(c => c.Email == strEmail40);

                //add the user to the role
                if (userManager.IsInRole(c40ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c40ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail41 = "tanner@ggmail.com";
            var c41 = new AppUser()
            {
                UserName = strEmail41,
                Email = strEmail41,
                FName = "Jeremy",
                MInitial = "S",
                LName = "Tanner",
                StreetAddress = "4347 Almstead",
                City = "Houston",
                State = States.TX,
                Zip = 77044,
                Phone = "(817)459-0929",
                Birthday = DateTime.Parse("5/21/1982")
            };

            //see if the user is already there
            AppUser c41ToAdd = db.Users.Where(c => c.Email == strEmail41).FirstOrDefault();
            if (c41ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c41, "kindly");
                c41ToAdd = db.Users.Single(c => c.Email == strEmail41);

                //add the user to the role
                if (userManager.IsInRole(c41ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c41ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail42 = "taylordjay@aool.com";
            var c42 = new AppUser()
            {
                UserName = strEmail42,
                Email = strEmail42,
                FName = "Allison",
                MInitial = "R",
                LName = "Taylor",
                StreetAddress = "467 Nueces St.",
                City = "Austin",
                State = States.TX,
                Zip = 78705,
                Phone = "(512)474-8452",
                Birthday = DateTime.Parse("1/8/1960")
            };

            //see if the user is already there
            AppUser c42ToAdd = db.Users.Where(c => c.Email == strEmail42).FirstOrDefault();
            if (c42ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c42, "instrument");
                c42ToAdd = db.Users.Single(c => c.Email == strEmail42);

                //add the user to the role
                if (userManager.IsInRole(c42ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c42ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail43 = "TayTaylor@aool.com";
            var c43 = new AppUser()
            {
                UserName = strEmail43,
                Email = strEmail43,
                FName = "Rachel",
                MInitial = "K",
                LName = "Taylor",
                StreetAddress = "345 Longview Dr.",
                City = "Austin",
                State = States.TX,
                Zip = 78705,
                Phone = "(512)451-2631",
                Birthday = DateTime.Parse("7/27/1975")
            };

            //see if the user is already there
            AppUser c43ToAdd = db.Users.Where(c => c.Email == strEmail43).FirstOrDefault();
            if (c43ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c43, "arched");
                c43ToAdd = db.Users.Single(c => c.Email == strEmail43);

                //add the user to the role
                if (userManager.IsInRole(c43ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c43ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail44 = "teefrank@hootmail.com";
            var c44 = new AppUser()
            {
                UserName = strEmail44,
                Email = strEmail44,
                FName = "Frank",
                MInitial = "J",
                LName = "Tee",
                StreetAddress = "5590 Lavell Dr",
                City = "Houston",
                State = States.TX,
                Zip = 77004,
                Phone = "(817)876-5543",
                Birthday = DateTime.Parse("4/6/1968")
            };

            //see if the user is already there
            AppUser c44ToAdd = db.Users.Where(c => c.Email == strEmail44).FirstOrDefault();
            if (c44ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c44, "median");
                c44ToAdd = db.Users.Single(c => c.Email == strEmail44);

                //add the user to the role
                if (userManager.IsInRole(c44ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c44ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail45 = "tuck33@ggmail.com";
            var c45 = new AppUser()
            {
                UserName = strEmail45,
                Email = strEmail45,
                FName = "Clent",
                MInitial = "J",
                LName = "Tucker",
                StreetAddress = "312 Main St.",
                City = "Dallas",
                State = States.TX,
                Zip = 75315,
                Phone = "(214)847-1154",
                Birthday = DateTime.Parse("5/19/1978")
            };

            //see if the user is already there
            AppUser c45ToAdd = db.Users.Where(c => c.Email == strEmail45).FirstOrDefault();
            if (c45ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c45, "approval");
                c45ToAdd = db.Users.Single(c => c.Email == strEmail45);

                //add the user to the role
                if (userManager.IsInRole(c45ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c45ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail46 = "avelasco@yaho.com";
            var c46 = new AppUser()
            {
                UserName = strEmail46,
                Email = strEmail46,
                FName = "Allen",
                MInitial = "G",
                LName = "Velasco",
                StreetAddress = "679 W. 4th",
                City = "Dallas",
                State = States.TX,
                Zip = 75207,
                Phone = "(214)398-5638",
                Birthday = DateTime.Parse("10/6/1963")
            };

            //see if the user is already there
            AppUser c46ToAdd = db.Users.Where(c => c.Email == strEmail46).FirstOrDefault();
            if (c46ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c46, "decorate");
                c46ToAdd = db.Users.Single(c => c.Email == strEmail46);

                //add the user to the role
                if (userManager.IsInRole(c46ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c46ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail47 = "westj@pioneer.net";
            var c47 = new AppUser()
            {
                UserName = strEmail47,
                Email = strEmail47,
                FName = "Jake",
                MInitial = "T",
                LName = "West",
                StreetAddress = "RR 3287",
                City = "Dallas",
                State = States.TX,
                Zip = 75323,
                Phone = "(214)847-5244",
                Birthday = DateTime.Parse("10/14/1993")
            };

            //see if the user is already there
            AppUser c47ToAdd = db.Users.Where(c => c.Email == strEmail47).FirstOrDefault();
            if (c47ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c47, "decorate");
                c47ToAdd = db.Users.Single(c => c.Email == strEmail47);

                //add the user to the role
                if (userManager.IsInRole(c47ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c47ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail48 = "louielouie@aool.com";
            var c48 = new AppUser()
            {
                UserName = strEmail48,
                Email = strEmail48,
                FName = "Louis",
                MInitial = "L",
                LName = "Winthorpe",
                StreetAddress = "2500 Padre Blvd",
                City = "Dallas",
                State = States.TX,
                Zip = 75220,
                Phone = "(214)565-0098",
                Birthday = DateTime.Parse("5/31/1952")
            };

            //see if the user is already there
            AppUser c48ToAdd = db.Users.Where(c => c.Email == strEmail48).FirstOrDefault();
            if (c48ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c48, "sturdy");
                c48ToAdd = db.Users.Single(c => c.Email == strEmail48);

                //add the user to the role
                if (userManager.IsInRole(c48ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c48ToAdd.Id, roleName);
                }
            }

            //create a user
            String strEmail49 = "rwood@voyager.net";
            var c49 = new AppUser()
            {
                UserName = strEmail49,
                Email = strEmail49,
                FName = "Reagan",
                MInitial = "B",
                LName = "Wood",
                StreetAddress = "447 Westlake Dr.",
                City = "Austin",
                State = States.TX,
                Zip = 78746,
                Phone = "(512)454-5242",
                Birthday = DateTime.Parse("4/24/1992")
            };

            //see if the user is already there
            AppUser c49ToAdd = db.Users.Where(c => c.Email == strEmail49).FirstOrDefault();
            if (c49ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(c49, "decorous");
                c49ToAdd = db.Users.Single(c => c.Email == strEmail49);

                //add the user to the role
                if (userManager.IsInRole(c49ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(c49ToAdd.Id, roleName);
                }
            }

        }

        public static void SeedEmployees(AppDbContext db)
        {
            //create a user manager to add users to databases
            UserManager<AppUser> userManager = new UserManager<AppUser>(new UserStore<AppUser>(db));
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            //create a role manager
            AppRoleManager roleManager = new AppRoleManager(new RoleStore<AppRole>(db));

            //create a customer role
            String roleName = "Employee";
            //check to see if the role exists
            if (roleManager.RoleExists(roleName) == false) //role doesn't exist
            {
                roleManager.Create(new AppRole(roleName));
            }

            //create an employee
            String strEmailE1 = "t.jacobs@longhornbank.neet";
            var e1 = new AppUser()
            {
                UserName = strEmailE1,
                Email = strEmailE1,
                FName = "Todd",
                MInitial = "L",
                LName = "Jacobs",
                StreetAddress = "4564 Elm St.",
                City = "Houston",
                State = States.TX,
                Zip = 77003,
                Phone = "(817)659-3544",
                Birthday = DateTime.Parse("1/1/1800")
            };
            //see if the user is already there
            AppUser e1ToAdd = db.Users.Where(e => e.Email == strEmailE1).FirstOrDefault();
            if (e1ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(e1, "society");
                e1ToAdd = db.Users.Single(c => c.Email == strEmailE1);

                //add the user to the role
                if (userManager.IsInRole(e1ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(e1ToAdd.Id, roleName);
                }
            }

            //create an employee
            String strEmailE2 = "e.rice@longhornbank.neet";
            var e2 = new AppUser()
            {
                UserName = strEmailE2,
                Email = strEmailE2,
                FName = "Eryn",
                MInitial = "M",
                LName = "Rice",
                StreetAddress = "3405 Rio Grande",
                City = "Dallas",
                State = States.TX,
                Zip = 75261,
                Phone = "(214)847-5583",
                Birthday = DateTime.Parse("1/1/1800")
            };
            //see if the user is already there
            AppUser e2ToAdd = db.Users.Where(e => e.Email == strEmailE2).FirstOrDefault();
            if (e2ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(e2, "ricearoni");
                e2ToAdd = db.Users.Single(c => c.Email == strEmailE2);

                //add the user to the role
                if (userManager.IsInRole(e2ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(e2ToAdd.Id, roleName);
                }
            }

            //create an employee
            String strEmailE3 = "b.ingram@longhornbank.neet";
            var e3 = new AppUser()
            {
                UserName = strEmailE3,
                Email = strEmailE3,
                FName = "Brad",
                MInitial = "S",
                LName = "Ingram",
                StreetAddress = "6548 La Posada Ct.",
                City = "Austin",
                State = States.TX,
                Zip = 78705,
                Phone = "(512)697-8613",
                Birthday = DateTime.Parse("1/1/1800")
            };
            //see if the user is already there
            AppUser e3ToAdd = db.Users.Where(e => e.Email == strEmailE3).FirstOrDefault();
            if (e3ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(e3, "ingram45");
                e3ToAdd = db.Users.Single(c => c.Email == strEmailE3);

                //add the user to the role
                if (userManager.IsInRole(e3ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(e3ToAdd.Id, roleName);
                }
            }

            //create an employee
            String strEmailE4 = "g.martinez@longhornbank.neet";
            var e4 = new AppUser()
            {
                UserName = strEmailE4,
                Email = strEmailE4,
                FName = "Gregory",
                MInitial = "R",
                LName = "Martinez",
                StreetAddress = "8295 Sunset Blvd.",
                City = "San Antonio",
                State = States.TX,
                Zip = 78239,
                Phone = "(210)578-8965",
                Birthday = DateTime.Parse("1/1/1800")
            };
            //see if the user is already there
            AppUser e4ToAdd = db.Users.Where(e => e.Email == strEmailE4).FirstOrDefault();
            if (e4ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(e4, "fungus");
                e4ToAdd = db.Users.Single(c => c.Email == strEmailE4);

                //add the user to the role
                if (userManager.IsInRole(e4ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(e4ToAdd.Id, roleName);
                }
            }

            //create an employee
            String strEmailE5 = "j.tanner@longhornbank.neet";
            var e5 = new AppUser()
            {
                UserName = strEmailE5,
                Email = strEmailE5,
                FName = "Jeremy",
                MInitial = "S",
                LName = "Tanner",
                StreetAddress = "4347 Almstead",
                City = "Austin",
                State = States.TX,
                Zip = 78761,
                Phone = "(512)945-7399",
                Birthday = DateTime.Parse("1/1/1800")
            };
            //see if the user is already there
            AppUser e5ToAdd = db.Users.Where(e => e.Email == strEmailE5).FirstOrDefault();
            if (e5ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(e5, "tanman");
                e5ToAdd = db.Users.Single(c => c.Email == strEmailE5);

                //add the user to the role
                if (userManager.IsInRole(e5ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(e5ToAdd.Id, roleName);
                }
            }

            //create an employee
            String strEmailE6 = "l.chung@longhornbank.neet";
            var e6 = new AppUser()
            {
                UserName = strEmailE6,
                Email = strEmailE6,
                FName = "Lisa",
                MInitial = "N",
                LName = "Chung",
                StreetAddress = "234 RR 12",
                City = "San Antonio",
                State = States.TX,
                Zip = 78268,
                Phone = "(210)698-3548",
                Birthday = DateTime.Parse("1/1/1800")
            };
            //see if the user is already there
            AppUser e6ToAdd = db.Users.Where(e => e.Email == strEmailE6).FirstOrDefault();
            if (e6ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(e6, "lisssa");
                e6ToAdd = db.Users.Single(c => c.Email == strEmailE6);

                //add the user to the role
                if (userManager.IsInRole(e6ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(e6ToAdd.Id, roleName);
                }
            }

            //create an employee
            String strEmailE7 = "w.loter@longhornbank.neet";
            var e7 = new AppUser()
            {
                UserName = strEmailE7,
                Email = strEmailE7,
                FName = "Wanda",
                MInitial = "K",
                LName = "Loter",
                StreetAddress = "3453 RR 3235",
                City = "Austin",
                State = States.TX,
                Zip = 78732,
                Phone = "(512)457-9845",
                Birthday = DateTime.Parse("1/1/1800")
            };
            //see if the user is already there
            AppUser e7ToAdd = db.Users.Where(e => e.Email == strEmailE7).FirstOrDefault();
            if (e7ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(e7, "lisssa");
                e7ToAdd = db.Users.Single(c => c.Email == strEmailE7);

                //add the user to the role
                if (userManager.IsInRole(e7ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(e7ToAdd.Id, roleName);
                }
            }

            //create an employee
            String strEmailE8 = "h.morales@longhornbank.neet";
            var e8 = new AppUser()
            {
                UserName = strEmailE8,
                Email = strEmailE8,
                FName = "Hector",
                MInitial = "N",
                LName = "Morales",
                StreetAddress = "4501 RR 140",
                City = "Houston",
                State = States.TX,
                Zip = 77031,
                Phone = "(817)745-8615",
                Birthday = DateTime.Parse("1/1/1800")
            };
            //see if the user is already there
            AppUser e8ToAdd = db.Users.Where(e => e.Email == strEmailE8).FirstOrDefault();
            if (e8ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(e8, "lisssa");
                e8ToAdd = db.Users.Single(c => c.Email == strEmailE8);

                //add the user to the role
                if (userManager.IsInRole(e8ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(e8ToAdd.Id, roleName);
                }
            }

            //create an employee
            String strEmailE9 = "m.rankin@longhornbank.neet";
            var e9 = new AppUser()
            {
                UserName = strEmailE9,
                Email = strEmailE9,
                FName = "Mary",
                MInitial = "T",
                LName = "Rankin",
                StreetAddress = "340 Second St",
                City = "Austin",
                State = States.TX,
                Zip = 78703,
                Phone = "(512)292-6966",
                Birthday = DateTime.Parse("1/1/1800")
            };
            //see if the user is already there
            AppUser e9ToAdd = db.Users.Where(e => e.Email == strEmailE9).FirstOrDefault();
            if (e9ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(e9, "rankmary");
                e9ToAdd = db.Users.Single(c => c.Email == strEmailE9);

                //add the user to the role
                if (userManager.IsInRole(e9ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(e9ToAdd.Id, roleName);
                }
            }

            //create an employee
            String strEmailE10 = "g.gonzalez@longhornbank.neet";
            var e10 = new AppUser()
            {
                UserName = strEmailE10,
                Email = strEmailE10,
                FName = "Gwen",
                MInitial = "J",
                LName = "Gonzalez",
                StreetAddress = "103 Manor Rd",
                City = "Dallas",
                State = States.TX,
                Zip = 75260,
                Phone = "(214)234-5566",
                Birthday = DateTime.Parse("1/1/1800")
            };
            //see if the user is already there
            AppUser e10ToAdd = db.Users.Where(e => e.Email == strEmailE10).FirstOrDefault();
            if (e10ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(e10, "offbeat");
                e10ToAdd = db.Users.Single(c => c.Email == strEmailE10);

                //add the user to the role
                if (userManager.IsInRole(e10ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(e10ToAdd.Id, roleName);
                }
            }
        }

        public static void SeedManagers(AppDbContext db)
        {
            //create a user manager to add users to databases
            UserManager<AppUser> userManager = new UserManager<AppUser>(new UserStore<AppUser>(db));
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            //create a role manager
            AppRoleManager roleManager = new AppRoleManager(new RoleStore<AppRole>(db));

            //create a customer role
            String roleName = "Manager";
            //check to see if the role exists
            if (roleManager.RoleExists(roleName) == false) //role doesn't exist
            {
                roleManager.Create(new AppRole(roleName));
            }

            //create an employee
            String strEmailM1 = "a.taylor@longhornbank.neet";
            var m1 = new AppUser()
            {
                UserName = strEmailM1,
                Email = strEmailM1,
                FName = "Allison",
                MInitial = "R",
                LName = "Taylor",
                StreetAddress = "467 Nueces St.",
                City = "Dallas",
                State = States.TX,
                Zip = 75237,
                Phone = "(214)896-5621",
                Birthday = DateTime.Parse("1/1/1800")
            };
            //see if the user is already there
            AppUser m1ToAdd = db.Users.Where(m => m.Email == strEmailM1).FirstOrDefault();
            if (m1ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(m1, "nostalgia");
                m1ToAdd = db.Users.Single(c => c.Email == strEmailM1);

                //add the user to the role
                if (userManager.IsInRole(m1ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(m1ToAdd.Id, roleName);
                }
            }

            //create an employee
            String strEmailM2 = "m.sheffield@longhornbank.neet";
            var m2 = new AppUser()
            {
                UserName = strEmailM2,
                Email = strEmailM2,
                FName = "Martin",
                MInitial = "J",
                LName = "Sheffield",
                StreetAddress = "3886 Avenue A",
                City = "Austin",
                State = States.TX,
                Zip = 78736,
                Phone = "(512)467-8821",
                Birthday = DateTime.Parse("1/1/1800")
            };
            //see if the user is already there
            AppUser m2ToAdd = db.Users.Where(m => m.Email == strEmailM2).FirstOrDefault();
            if (m2ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(m2, "longhorns");
                m2ToAdd = db.Users.Single(c => c.Email == strEmailM2);

                //add the user to the role
                if (userManager.IsInRole(m2ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(m2ToAdd.Id, roleName);
                }
            }

            //create an employee
            String strEmailM3 = "j.macleod@longhornbank.neet";
            var m3 = new AppUser()
            {
                UserName = strEmailM3,
                Email = strEmailM3,
                FName = "Jennifer",
                MInitial = "D",
                LName = "MacLeod",
                StreetAddress = "2504 Far West Blvd.",
                City = "Austin",
                State = States.TX,
                Zip = 78731,
                Phone = "(512)465-3365",
                Birthday = DateTime.Parse("1/1/1800")
            };
            //see if the user is already there
            AppUser m3ToAdd = db.Users.Where(m => m.Email == strEmailM3).FirstOrDefault();
            if (m3ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(m3, "smitty");
                m3ToAdd = db.Users.Single(c => c.Email == strEmailM2);

                //add the user to the role
                if (userManager.IsInRole(m3ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(m3ToAdd.Id, roleName);
                }
            }

            //create an employee
            String strEmailM4 = "m.rhodes@longhornbank.neet";
            var m4 = new AppUser()
            {
                UserName = strEmailM4,
                Email = strEmailM4,
                FName = "Megan",
                MInitial = "C",
                LName = "Rhodes",
                StreetAddress = "4587 Enfield Rd.",
                City = "San Antonio",
                State = States.TX,
                Zip = 78293,
                Phone = "(210)244-9976",
                Birthday = DateTime.Parse("1/1/1800")
            };
            //see if the user is already there
            AppUser m4ToAdd = db.Users.Where(m => m.Email == strEmailM4).FirstOrDefault();
            if (m4ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(m4, "nostalgia");
                m4ToAdd = db.Users.Single(c => c.Email == strEmailM4);

                //add the user to the role
                if (userManager.IsInRole(m4ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(m4ToAdd.Id, roleName);
                }
            }

            //create an employee
            String strEmailM5 = "e.stuart@longhornbank.neet";
            var m5 = new AppUser()
            {
                UserName = strEmailM5,
                Email = strEmailM5,
                FName = "Eric",
                MInitial = "F",
                LName = "Stuart",
                StreetAddress = "5576 Toro Ring",
                City = "San Antonio",
                State = States.TX,
                Zip = 78279,
                Phone = "(210)534-4627",
                Birthday = DateTime.Parse("1/1/1800")
            };
            //see if the user is already there
            AppUser m5ToAdd = db.Users.Where(m => m.Email == strEmailM5).FirstOrDefault();
            if (m5ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(m5, "stewboy");
                m5ToAdd = db.Users.Single(c => c.Email == strEmailM5);

                //add the user to the role
                if (userManager.IsInRole(m5ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(m5ToAdd.Id, roleName);
                }
            }

            //create an employee
            String strEmailM6 = "l.swanson@longhornbank.neet";
            var m6 = new AppUser()
            {
                UserName = strEmailM6,
                Email = strEmailM6,
                FName = "Leon",
                LName = "Swanson",
                StreetAddress = "245 River Rd",
                City = "Austin",
                State = States.TX,
                Zip = 78731,
                Phone = "(512)474-8138",
                Birthday = DateTime.Parse("1/1/1800")
            };
            //see if the user is already there
            AppUser m6ToAdd = db.Users.Where(m => m.Email == strEmailM6).FirstOrDefault();
            if (m6ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(m6, "swansong");
                m6ToAdd = db.Users.Single(c => c.Email == strEmailM6);

                //add the user to the role
                if (userManager.IsInRole(m6ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(m6ToAdd.Id, roleName);
                }
            }

            //create an employee
            String strEmailM7 = "j.white@longhornbank.neet";
            var m7 = new AppUser()
            {
                UserName = strEmailM7,
                Email = strEmailM7,
                FName = "Jason",
                MInitial = "M",
                LName = "White",
                StreetAddress = "12 Valley View",
                City = "Houston",
                State = States.TX,
                Zip = 77045,
                Phone = "(817)495-5201",
                Birthday = DateTime.Parse("1/1/1800")
            };
            //see if the user is already there
            AppUser m7ToAdd = db.Users.Where(m => m.Email == strEmailM7).FirstOrDefault();
            if (m7ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(m7, "evanescent");
                m7ToAdd = db.Users.Single(c => c.Email == strEmailM7);

                //add the user to the role
                if (userManager.IsInRole(m7ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(m7ToAdd.Id, roleName);
                }
            }

            //create an employee
            String strEmailM8 = "w.montgomery@longhornbank.neet";
            var m8 = new AppUser()
            {
                UserName = strEmailM8,
                Email = strEmailM8,
                FName = "Wilda",
                MInitial = "K",
                LName = "Montgomery",
                StreetAddress = "210 Blanco Dr",
                City = "Houston",
                State = States.TX,
                Zip = 77030,
                Phone = "(817)874-6718",
                Birthday = DateTime.Parse("1/1/1800")
            };
            //see if the user is already there
            AppUser m8ToAdd = db.Users.Where(m => m.Email == strEmailM8).FirstOrDefault();
            if (m8ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(m8, "monty3");
                m8ToAdd = db.Users.Single(c => c.Email == strEmailM8);

                //add the user to the role
                if (userManager.IsInRole(m8ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(m8ToAdd.Id, roleName);
                }
            }

            //create an employee
            String strEmailM9 = "l.walker@longhornbank.neet";
            var m9 = new AppUser()
            {
                UserName = strEmailM9,
                Email = strEmailM9,
                FName = "Larry",
                MInitial = "G",
                LName = "Walker",
                StreetAddress = "9 Bison Circle",
                City = "Dallas",
                State = States.TX,
                Zip = 75238,
                Phone = "(214)312-5897",
                Birthday = DateTime.Parse("1/1/1800")
            };
            //see if the user is already there
            AppUser m9ToAdd = db.Users.Where(m => m.Email == strEmailM9).FirstOrDefault();
            if (m9ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(m9, "walkamile");
                m9ToAdd = db.Users.Single(c => c.Email == strEmailM9);

                //add the user to the role
                if (userManager.IsInRole(m9ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(m9ToAdd.Id, roleName);
                }
            }

            //create an employee
            String strEmailM10 = "g.chang@longhornbank.neet";
            var m10 = new AppUser()
            {
                UserName = strEmailM10,
                Email = strEmailM10,
                FName = "George",
                MInitial = "M",
                LName = "Chang",
                StreetAddress = "9003 Joshua St",
                City = "San Antonio",
                State = States.TX,
                Zip = 78260,
                Phone = "(210)345-0925",
                Birthday = DateTime.Parse("1/1/1800")
            };
            //see if the user is already there
            AppUser m10ToAdd = db.Users.Where(m => m.Email == strEmailM10).FirstOrDefault();
            if (m10ToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(m10, "changalang");
                m10ToAdd = db.Users.Single(c => c.Email == strEmailM10);

                //add the user to the role
                if (userManager.IsInRole(m10ToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(m10ToAdd.Id, roleName);
                }
            }
        }

        public static void SeedAccounts(AppDbContext db)
        {
            //create a new STOCK account
            Int32 intAccNum0 = 1000000000;
            var account0 = new Stock()
            {
                StockName = "Shan's Stock",
                StockBalance = 0,
                TotalBalance = 0,
                Status = true,
                AccountNumber = intAccNum0,
                SecureNumber = "XXXXXX0000"
            };

            //see if the account is already there
            Stock account0ToAdd = db.Stocks.Where(a => a.AccountNumber == intAccNum0).FirstOrDefault();
            if (account0ToAdd == null) //this account doesn't exist yet
            {
                var owner = db.Users.FirstOrDefault(c => c.Email == "Dixon@aool.com");
                account0.Owner = owner;

                //add or update user
                db.Stocks.AddOrUpdate(s => s.StockID, account0);
            }

            db.SaveChanges();

            //create a new SAVINGS account
            Int32 intAccNum1 = 1000000001;
            var account1 = new Saving()
            {
                SavingName = "William's Savings",
                SavingBalance = 40035.50m,
                Status = true,
                AccountNumber = intAccNum1,
                SecureNumber = "XXXXXX0001"
            };

            //see if the account is already there
            Saving account1ToAdd = db.Savings.Where(a => a.AccountNumber == intAccNum1).FirstOrDefault();
            if (account1ToAdd == null) //this account doesn't exist yet
            {
                var owner = db.Users.FirstOrDefault(c => c.Email == "willsheff@email.com");
                account1.Owner = owner;

                db.Savings.AddOrUpdate(s => s.SavingID, account1);
            }

            db.SaveChanges();

            //create a new CHECKING account
            Int32 intAccNum2 = 1000000002;
            var account2 = new Checking()
            {
                CheckingName = "Gregory's Checking",
                CheckingBalance = 39779.49m,
                Status = true,
                AccountNumber = intAccNum2,
                SecureNumber = "XXXXXX0002"
            };

            //see if the account is already there
            Checking account2ToAdd = db.Checkings.Where(a => a.AccountNumber == intAccNum2).FirstOrDefault();
            if (account2ToAdd == null) //this account doesn't exist yet
            {
                var owner = db.Users.FirstOrDefault(c => c.Email == "smartinmartin.Martin@aool.com");
                account2.Owner = owner;

                db.Checkings.AddOrUpdate(s => s.CheckingID, account2);
            }

            db.SaveChanges();

            //create a new CHECKING account
            Int32 intAccNum3 = 1000000003;
            var account3 = new Checking()
            {
                CheckingName = "Allen's Checking",
                CheckingBalance = 47277.33m,
                Status = true,
                AccountNumber = intAccNum3,
                SecureNumber = "XXXXXX0003"
            };

            //see if the account is already there
            Checking account3ToAdd = db.Checkings.Where(a => a.AccountNumber == intAccNum3).FirstOrDefault();
            if (account3ToAdd == null) //this account doesn't exist yet
            {
                var owner = db.Users.FirstOrDefault(c => c.Email == "avelasco@yaho.com");
                account3.Owner = owner;

                db.Checkings.AddOrUpdate(s => s.CheckingID, account3);
            }

            db.SaveChanges();

            //create a new CHECKING account
            Int32 intAccNum4 = 1000000004;
            var account4 = new Checking()
            {
                CheckingName = "Reagan's Checking",
                CheckingBalance = 70812.15m,
                Status = true,
                AccountNumber = intAccNum4,
                SecureNumber = "XXXXXX0004"
            };

            //see if the account is already there
            Checking account4ToAdd = db.Checkings.Where(a => a.AccountNumber == intAccNum4).FirstOrDefault();
            if (account4ToAdd == null) //this account doesn't exist yet
            {
                var owner = db.Users.FirstOrDefault(c => c.Email == "rwood@voyager.net");
                account4.Owner = owner;

                db.Checkings.AddOrUpdate(s => s.CheckingID, account4);
            }

            db.SaveChanges();

            //create a new SAVINGS account
            Int32 intAccNum5 = 1000000005;
            var account5 = new Saving()
            {
                SavingName = "Kelly's Savings",
                SavingBalance = 21901.97m,
                Status = true,
                AccountNumber = intAccNum5,
                SecureNumber = "XXXXXX0005"
            };

            //see if the account is already there
            Saving account5ToAdd = db.Savings.Where(a => a.AccountNumber == intAccNum5).FirstOrDefault();
            if (account5ToAdd == null) //this account doesn't exist yet
            {
                var owner = db.Users.FirstOrDefault(c => c.Email == "nelson.Kelly@aool.com");
                account5.Owner = owner;

                db.Savings.AddOrUpdate(s => s.SavingID, account5);
            }

            db.SaveChanges();

            //create a new CHECKING account
            Int32 intAccNum6 = 1000000006;
            var account6 = new Checking()
            {
                CheckingName = "Eryn's Checking",
                CheckingBalance = 70480.99m,
                Status = true,
                AccountNumber = intAccNum6,
                SecureNumber = "XXXXXX0006"
            };

            //see if the account is already there
            Checking account6ToAdd = db.Checkings.Where(a => a.AccountNumber == intAccNum6).FirstOrDefault();
            if (account6ToAdd == null) //this account doesn't exist yet
            {
                var owner = db.Users.FirstOrDefault(c => c.Email == "erynrice@aool.com");
                account6.Owner = owner;

                db.Checkings.AddOrUpdate(s => s.CheckingID, account6);
            }

            db.SaveChanges();

            //create a new SAVINGS account
            Int32 intAccNum7 = 1000000007;
            var account7 = new Saving()
            {
                SavingName = "Jake's Savings",
                SavingBalance = 7916.4m,
                Status = true,
                AccountNumber = intAccNum7,
                SecureNumber = "XXXXXX0007"
            };

            //see if the account is already there
            Saving account7ToAdd = db.Savings.Where(a => a.AccountNumber == intAccNum7).FirstOrDefault();
            if (account7ToAdd == null) //this account doesn't exist yet
            {
                var owner = db.Users.FirstOrDefault(c => c.Email == "westj@pioneer.net");
                account7.Owner = owner;

                db.Savings.AddOrUpdate(s => s.SavingID, account7);
            }

            db.SaveChanges();

            //create a new STOCK account
            Int32 intAccNum8 = 1000000008;
            var account8 = new Stock()
            {
                StockName = "Michelle's Stock",
                StockBalance = 0,
                TotalBalance = 0,
                Status = true,
                AccountNumber = intAccNum8,
                SecureNumber = "XXXXXX0008"
            };

            //see if the account is already there
            Stock account8ToAdd = db.Stocks.Where(a => a.AccountNumber == intAccNum8).FirstOrDefault();
            if (account8ToAdd == null) //this account doesn't exist yet
            {
                var owner = db.Users.FirstOrDefault(c => c.Email == "mb@aool.com");
                account8.Owner = owner;

                //add or update user
                db.Stocks.AddOrUpdate(s => s.StockID, account8);
            }

            db.SaveChanges();

            //create a new SAVINGS account
            Int32 intAccNum9 = 1000000009;
            var account9 = new Saving()
            {
                SavingName = "Jeffrey's Savings",
                SavingBalance = 69576.83m,
                Status = true,
                AccountNumber = intAccNum9,
                SecureNumber = "XXXXXX0009"
            };

            //see if the account is already there
            Saving account9ToAdd = db.Savings.Where(a => a.AccountNumber == intAccNum9).FirstOrDefault();
            if (account9ToAdd == null) //this account doesn't exist yet
            {
                var owner = db.Users.FirstOrDefault(c => c.Email == "jeff@ggmail.com");
                account9.Owner = owner;

                db.Savings.AddOrUpdate(s => s.SavingID, account9);
            }

            db.SaveChanges();

            //create a new STOCK account
            Int32 intAccNum10 = 1000000010;
            var account10 = new Stock()
            {
                StockName = "Kelly's Stock",
                StockBalance = 0,
                TotalBalance = 0,
                Status = true,
                AccountNumber = intAccNum10,
                SecureNumber = "XXXXXX0010"
            };

            //see if the account is already there
            Stock account10ToAdd = db.Stocks.Where(a => a.AccountNumber == intAccNum10).FirstOrDefault();
            if (account10ToAdd == null) //this account doesn't exist yet
            {
                var owner = db.Users.FirstOrDefault(c => c.Email == "nelson.Kelly@aool.com");
                account10.Owner = owner;

                //add or update user
                db.Stocks.AddOrUpdate(s => s.StockID, account10);
            }

            db.SaveChanges();

            //create a new CHECKING account
            Int32 intAccNum11 = 1000000011;
            var account11 = new Checking()
            {
                CheckingName = "Eryn's Checking 2",
                CheckingBalance = 30279.33m,
                Status = true,
                AccountNumber = intAccNum11,
                SecureNumber = "XXXXXX0011"
            };

            //see if the account is already there
            Checking account11ToAdd = db.Checkings.Where(a => a.AccountNumber == intAccNum11).FirstOrDefault();
            if (account11ToAdd == null) //this account doesn't exist yet
            {
                var owner = db.Users.FirstOrDefault(c => c.Email == "erynrice@aool.com");
                account11.Owner = owner;

                db.Checkings.AddOrUpdate(s => s.CheckingID, account11);
            }

            db.SaveChanges();

            //create a new IRA account
            Int32 intAccNum12 = 1000000012;
            var account12 = new IRA()
            {
                IRAName = "Jennifer's IRA",
                IRABalance = 5000.00m,
                IRACumulative = 0,
                Status = true,
                AccountNumber = intAccNum12,
                SecureNumber = "XXXXXX0012"
            };

            //see if the account is already there
            IRA account12ToAdd = db.IRAs.Where(a => a.AccountNumber == intAccNum12).FirstOrDefault();
            if (account12ToAdd == null) //this account doesn't exist yet
            {
                var owner = db.Users.FirstOrDefault(c => c.Email == "mackcloud@pimpdaddy.com");
                account12.Owner = owner;

                db.IRAs.AddOrUpdate(s => s.IRAID, account12);
            }

            db.SaveChanges();

            //create a new SAVINGS account
            Int32 intAccNum13 = 1000000013;
            var account13 = new Saving()
            {
                SavingName = "Sarah's Savings",
                SavingBalance = 11958.08m,
                Status = true,
                AccountNumber = intAccNum13,
                SecureNumber = "XXXXXX0013"
            };

            //see if the account is already there
            Saving account13ToAdd = db.Savings.Where(a => a.AccountNumber == intAccNum13).FirstOrDefault();
            if (account13ToAdd == null) //this account doesn't exist yet
            {
                var owner = db.Users.FirstOrDefault(c => c.Email == "ss34@ggmail.com");
                account13.Owner = owner;

                db.Savings.AddOrUpdate(s => s.SavingID, account13);
            }

            db.SaveChanges();

            //create a new SAVINGS account
            Int32 intAccNum14 = 1000000014;
            var account14 = new Saving()
            {
                SavingName = "Jeremy's Savings",
                SavingBalance = 72990.47m,
                Status = true,
                AccountNumber = intAccNum14,
                SecureNumber = "XXXXXX0014"
            };

            //see if the account is already there
            Saving account14ToAdd = db.Savings.Where(a => a.AccountNumber == intAccNum14).FirstOrDefault();
            if (account14ToAdd == null) //this account doesn't exist yet
            {
                var owner = db.Users.FirstOrDefault(c => c.Email == "tanner@ggmail.com");
                account14.Owner = owner;

                db.Savings.AddOrUpdate(s => s.SavingID, account14);
            }

            db.SaveChanges();

            //create a new SAVINGS account
            Int32 intAccNum15 = 1000000015;
            var account15 = new Saving()
            {
                SavingName = "Elizabeth's Savings",
                SavingBalance = 7417.2m,
                Status = true,
                AccountNumber = intAccNum15,
                SecureNumber = "XXXXXX0015"
            };

            //see if the account is already there
            Saving account15ToAdd = db.Savings.Where(a => a.AccountNumber == intAccNum15).FirstOrDefault();
            if (account15ToAdd == null) //this account doesn't exist yet
            {
                var owner = db.Users.FirstOrDefault(c => c.Email == "liz@ggmail.com");
                account15.Owner = owner;

                db.Savings.AddOrUpdate(s => s.SavingID, account15);
            }

            db.SaveChanges();

            //create a new IRA account
            Int32 intAccNum16 = 1000000016;
            var account16 = new IRA()
            {
                IRAName = "Allen's IRA",
                IRABalance = 5000.00m,
                IRACumulative = 0,
                Status = true,
                AccountNumber = intAccNum16,
                SecureNumber = "XXXXXX0016"
            };

            //see if the account is already there
            IRA account16ToAdd = db.IRAs.Where(a => a.AccountNumber == intAccNum16).FirstOrDefault();
            if (account16ToAdd == null) //this account doesn't exist yet
            {
                var owner = db.Users.FirstOrDefault(c => c.Email == "ra@aoo.com");
                account16.Owner = owner;

                db.IRAs.AddOrUpdate(s => s.IRAID, account16);
            }

            db.SaveChanges();

            //create a new STOCK account
            Int32 intAccNum17 = 1000000017;
            var account17 = new Stock()
            {
                StockName = "John's Stock",
                StockBalance = 0,
                TotalBalance = 0,
                Status = true,
                AccountNumber = intAccNum17,
                SecureNumber = "XXXXXX0017"
            };

            //see if the account is already there
            Stock account17ToAdd = db.Stocks.Where(a => a.AccountNumber == intAccNum17).FirstOrDefault();
            if (account17ToAdd == null) //this account doesn't exist yet
            {
                var owner = db.Users.FirstOrDefault(c => c.Email == "johnsmith187@aool.com");
                account17.Owner = owner;

                //add or update user
                db.Stocks.AddOrUpdate(s => s.StockID, account17);
            }

            db.SaveChanges();

            //create a new SAVINGS account
            Int32 intAccNum18 = 1000000018;
            var account18 = new Saving()
            {
                SavingName = "Clarence's Savings",
                SavingBalance = 1642.82m,
                Status = true,
                AccountNumber = intAccNum18,
                SecureNumber = "XXXXXX0018"
            };

            //see if the account is already there
            Saving account18ToAdd = db.Savings.Where(a => a.AccountNumber == intAccNum18).FirstOrDefault();
            if (account18ToAdd == null) //this account doesn't exist yet
            {
                var owner = db.Users.FirstOrDefault(c => c.Email == "mclarence@aool.com");
                account18.Owner = owner;

                db.Savings.AddOrUpdate(s => s.SavingID, account18);
            }

            db.SaveChanges();

            //create a new CHECKING account
            Int32 intAccNum19 = 1000000019;
            var account19 = new Checking()
            {
                CheckingName = "Sarah's Checking",
                CheckingBalance = 84421.45m,
                Status = true,
                AccountNumber = intAccNum19,
                SecureNumber = "XXXXXX0019"
            };

            //see if the account is already there
            Checking account19ToAdd = db.Checkings.Where(a => a.AccountNumber == intAccNum19).FirstOrDefault();
            if (account19ToAdd == null) //this account doesn't exist yet
            {
                var owner = db.Users.FirstOrDefault(c => c.Email == "ss34@ggmail.com");
                account19.Owner = owner;

                db.Checkings.AddOrUpdate(s => s.CheckingID, account19);
            }

            db.SaveChanges();


        }

        public static void SeedPayee(AppDbContext db)
        {
            //create a new payee
            var payee1 = new Payee()
            {
                Name = "City of Austin Water",
                StreeAddress = "113 South Congress Ave.",
                City = "Austin",
                State = States.TX,
                Zip = 78710,
                Phone = "(512)664-5558",
                Type = Models.Type.Utilities
            };

            db.Payees.AddOrUpdate(s => s.Name, payee1);
            db.SaveChanges();
        
            //create a new payee
            var payee2 = new Payee()
            {
                Name = "Reliant Energy",
                StreeAddress = "3500 E. Interstate 10",
                City = "Houston",
                State = States.TX,
                Zip = 77099,
                Phone = "(713)554-6697",
                Type = Models.Type.Utilities
            };

            db.Payees.AddOrUpdate(s => s.Name, payee2);
            db.SaveChanges();

            //create a new payee
            var payee3 = new Payee()
            {
                Name = "Lee Properties",
                StreeAddress = "2500 Salado",
                City = "Austin",
                State = States.TX,
                Zip = 78705,
                Phone = "(512)445-3312",
                Type = Models.Type.Rent
            };

            db.Payees.AddOrUpdate(s => s.Name, payee3);
            db.SaveChanges();

            //create a new payee
            var payee4 = new Payee()
            {
                Name = "Capital One",
                StreeAddress = "1299 Fargo Blvd.",
                City = "Cheyenne",
                State = States.WY,
                Zip = 82001,
                Phone = "(530)221-5542",
                Type = Models.Type.CreditCard
            };

            db.Payees.AddOrUpdate(s => s.Name, payee4);
            db.SaveChanges();

            //create a new payee
            var payee5 = new Payee()
            {
                Name = "Vanguard Title",
                StreeAddress = "10976 Interstate 35 South",
                City = "Austin",
                State = States.TX,
                Zip = 78745,
                Phone = "(512)865-4951",
                Type = Models.Type.Mortgage
            };

            db.Payees.AddOrUpdate(s => s.Name, payee5);
            db.SaveChanges();

            //create a new payee
            var payee6 = new Payee()
            {
                Name = "Lawn Care of Texas",
                StreeAddress = "4473 W. 3rd Street",
                City = "Austin",
                State = States.TX,
                Zip = 78712,
                Phone = "(512)336-5247",
                Type = Models.Type.Other
            };  

            db.Payees.AddOrUpdate(s => s.Name, payee6);
            db.SaveChanges();
        }

        public static void SeedPortfolio(AppDbContext db)
        {
            //new stockquote
            var p1 = new StockQuote()
            {
                Symbol = "GOOG",
                Name = "Alphabet Inc.",
                StockType = StockType.Ordinary_Stocks,
                PurchaseFee = 25
            };
            db.StockQuotes.AddOrUpdate(p => p.StockQuoteID, p1);
            db.SaveChanges();

            //new stockquote
            var p2 = new StockQuote()
            {
                Symbol = "AAPL",
                Name = "Apple Inc.",
                StockType = StockType.Ordinary_Stocks,
                PurchaseFee = 40
            };
            db.StockQuotes.AddOrUpdate(p => p.StockQuoteID, p2);
            db.SaveChanges();

            //new stockquote
            var p3 = new StockQuote()
            {
                Symbol = "AMZN",
                Name = "Amazon.com Inc.",
                StockType = StockType.Ordinary_Stocks,
                PurchaseFee = 15
            };
            db.StockQuotes.AddOrUpdate(p => p.StockQuoteID, p3);
            db.SaveChanges();

            //new stockquote
            var p4 = new StockQuote()
            {
                Symbol = "LUV",
                Name = "Southwest Airlines",
                StockType = StockType.Ordinary_Stocks,
                PurchaseFee = 35
            };
            db.StockQuotes.AddOrUpdate(p => p.StockQuoteID, p4);
            db.SaveChanges();

            //new stockquote
            var p5 = new StockQuote()
            {
                Symbol = "TXN",
                Name = "Texas Instruments",
                StockType = StockType.Ordinary_Stocks,
                PurchaseFee = 15
            };
            db.StockQuotes.AddOrUpdate(p => p.StockQuoteID, p5);
            db.SaveChanges();

            //new stockquote
            var p6 = new StockQuote()
            {
                Symbol = "HSY",
                Name = "The Hershey Company",
                StockType = StockType.Ordinary_Stocks,
                PurchaseFee = 25
            };
            db.StockQuotes.AddOrUpdate(p => p.StockQuoteID, p6);
            db.SaveChanges();

            //new stockquote
            var p7 = new StockQuote()
            {
                Symbol = "V",
                Name = "Visa Inc.",
                StockType = StockType.Ordinary_Stocks,
                PurchaseFee = 10
            };
            db.StockQuotes.AddOrUpdate(p => p.StockQuoteID, p7);
            db.SaveChanges();

            //new stockquote
            var p8 = new StockQuote()
            {
                Symbol = "NKE",
                Name = "Nike",
                StockType = StockType.Ordinary_Stocks,
                PurchaseFee = 30
            };
            db.StockQuotes.AddOrUpdate(p => p.StockQuoteID, p8);
            db.SaveChanges();

            //new stockquote
            var p9 = new StockQuote()
            {
                Symbol = "VWO",
                Name = "Vanguard Emerging Markets",
                StockType = StockType.Exchange_Traded_Funds,
                PurchaseFee = 20
            };
            db.StockQuotes.AddOrUpdate(p => p.StockQuoteID, p9);
            db.SaveChanges();

            //new stockquote
            var p10 = new StockQuote()
            {
                Symbol = "CORN",
                Name = "Corn",
                StockType = StockType.Futures_Shares,
                PurchaseFee = 10
            };
            db.StockQuotes.AddOrUpdate(p => p.StockQuoteID, p10);
            db.SaveChanges();

            //new stockquote
            var p11 = new StockQuote()
            {
                Symbol = "F",
                Name = "Ford Motor Company",
                StockType = StockType.Ordinary_Stocks,
                PurchaseFee = 10
            };
            db.StockQuotes.AddOrUpdate(p => p.StockQuoteID, p11);
            db.SaveChanges();

            //new stockquote
            var p12 = new StockQuote()
            {
                Symbol = "BAC",
                Name = "Bank of America Corporation",
                StockType = StockType.Ordinary_Stocks,
                PurchaseFee = 10
            };
            db.StockQuotes.AddOrUpdate(p => p.StockQuoteID, p12);
            db.SaveChanges();

            //new stockquote
            var p13 = new StockQuote()
            {
                Symbol = "VNQ",
                Name = "Vanguard REIT ETF",
                StockType = StockType.Exchange_Traded_Funds,
                PurchaseFee = 15
            };
            db.StockQuotes.AddOrUpdate(p => p.StockQuoteID, p13);
            db.SaveChanges();

            //new stockquote
            var p14 = new StockQuote()
            {
                Symbol = "KMX",
                Name = "CarMax, Inc.",
                StockType = StockType.Ordinary_Stocks,
                PurchaseFee = 15
            };
            db.StockQuotes.AddOrUpdate(p => p.StockQuoteID, p14);
            db.SaveChanges();

            //new stockquote
            var p15 = new StockQuote()
            {
                Symbol = "DIA",
                Name = "Dow Jones Industrial Average Index Fund",
                StockType = StockType.Index_Funds,
                PurchaseFee = 25
            };
            db.StockQuotes.AddOrUpdate(p => p.StockQuoteID, p15);
            db.SaveChanges();

            //new stockquote
            var p16 = new StockQuote()
            {
                Symbol = "SPY",
                Name = "S&P 500 Index Fund",
                StockType = StockType.Index_Funds,
                PurchaseFee = 25
            };
            db.StockQuotes.AddOrUpdate(p => p.StockQuoteID, p16);
            db.SaveChanges();

            //new stockquote
            var p17 = new StockQuote()
            {
                Symbol = "BEN",
                Name = "Franklin Resources, Inc.",
                StockType = StockType.Ordinary_Stocks,
                PurchaseFee = 25
            };
            db.StockQuotes.AddOrUpdate(p => p.StockQuoteID, p17);
            db.SaveChanges();

            //new stockquote
            var p18 = new StockQuote()
            {
                Symbol = "PGSCX",
                Name = "Pacific Advisors Small Cap Value Fund",
                StockType = StockType.Mutual_Funds,
                PurchaseFee = 15
            };
            db.StockQuotes.AddOrUpdate(p => p.StockQuoteID, p18);
            db.SaveChanges();
        }



        public static void SeedTransactions(AppDbContext db)
        {
            //create new initial transaction
            var t0 = new Transaction()
            {
                Description = "Initial Transaction",
                TransactionType = TransactionType.deposit,
                Amount = 0,
                Date = DateTime.Parse("1/1/2016"),
                TransactionNumber = 1,
                Owner = db.Users.FirstOrDefault(c => c.Email == "Dixon@aool.com"),
                Stock = db.Stocks.FirstOrDefault(c => c.AccountNumber == 1000000000)
            };

            db.Transactions.AddOrUpdate(t => t.TransactionID, t0);
            db.SaveChanges();

            //create new initial transaction
            var account1 = db.Savings.Where(c => c.AccountNumber == 1000000001).FirstOrDefault();
            var t1 = new Transaction()
            {
                Description = "Initial Transaction",
                TransactionType = TransactionType.deposit,
                Amount = 40035.5m,
                Date = DateTime.Parse("1/1/2016"),
                TransactionNumber = 1,
                Owner = db.Users.FirstOrDefault(c => c.Email == "willsheff@email.com"),
                Savings = new List<Saving>(),
                Checkings = new List<Checking>()
            };
            t1.Savings.Add(account1);
            db.Transactions.AddOrUpdate(t => t.TransactionID, t1);
            db.SaveChanges();

            //create new initial transaction
            var account2 = db.Checkings.Where(c => c.AccountNumber == 1000000002).FirstOrDefault();
            var t2 = new Transaction()
            {
                Description = "Initial Transaction",
                TransactionType = TransactionType.deposit,
                Amount = 39779.49m,
                Date = DateTime.Parse("1/1/2016"),
                TransactionNumber = 1,
                Owner = db.Users.FirstOrDefault(c => c.Email == "smartinmartin.Martin@aool.com"),
                Savings = new List<Saving>(),
                Checkings = new List<Checking>()
            };
            t2.Checkings.Add(account2);
            db.Transactions.AddOrUpdate(t => t.TransactionID, t2);
            db.SaveChanges();

            //create new initial transaction
            var account3 = db.Checkings.Where(c => c.AccountNumber == 1000000003).FirstOrDefault();
            var t3 = new Transaction()
            {
                Description = "Initial Transaction",
                TransactionType = TransactionType.deposit,
                Amount = 47277.33m,
                Date = DateTime.Parse("1/1/2016"),
                TransactionNumber = 1,
                Owner = db.Users.FirstOrDefault(c => c.Email == "avelasco@yaho.com"),
                Savings = new List<Saving>(),
                Checkings = new List<Checking>()
            };
            t3.Checkings.Add(account3);
            db.Transactions.AddOrUpdate(t => t.TransactionID, t3);
            db.SaveChanges();

            //create new initial transaction
            var account4 = db.Checkings.Where(c => c.AccountNumber == 1000000004).FirstOrDefault();
            var t4 = new Transaction()
            {
                Description = "Initial Transaction",
                TransactionType = TransactionType.deposit,
                Amount = 70812.15m,
                Date = DateTime.Parse("1/1/2016"),
                TransactionNumber = 1,
                Owner = db.Users.FirstOrDefault(c => c.Email == "rwood@voyager.net"),
                Savings = new List<Saving>(),
                Checkings = new List<Checking>()
            };
            t4.Checkings.Add(account4);
            db.Transactions.AddOrUpdate(t => t.TransactionID, t4);
            db.SaveChanges();

            //create new initial transaction
            var account5 = db.Savings.Where(c => c.AccountNumber == 1000000005).FirstOrDefault();
            var t5 = new Transaction()
            {
                Description = "Initial Transaction",
                TransactionType = TransactionType.deposit,
                Amount = 21901.97m,
                Date = DateTime.Parse("1/1/2016"),
                TransactionNumber = 1,
                Owner = db.Users.FirstOrDefault(c => c.Email == "nelson.Kelly@aool.com"),
                Savings = new List<Saving>(),
                Checkings = new List<Checking>()
            };
            t5.Savings.Add(account5);
            db.Transactions.AddOrUpdate(t => t.TransactionID, t5);
            db.SaveChanges();

            //create new initial transaction
            var account6 = db.Checkings.Where(c => c.AccountNumber == 1000000006).FirstOrDefault();
            var t6 = new Transaction()
            {
                Description = "Initial Transaction",
                TransactionType = TransactionType.deposit,
                Amount = 70480.99m,
                Date = DateTime.Parse("1/1/2016"),
                TransactionNumber = 1,
                Owner = db.Users.FirstOrDefault(c => c.Email == "erynrice@aool.com"),
                Savings = new List<Saving>(),
                Checkings = new List<Checking>()
            };
            t6.Checkings.Add(account6);
            db.Transactions.AddOrUpdate(t => t.TransactionID, t6);
            db.SaveChanges();

            //create new initial transaction
            var account7 = db.Savings.Where(c => c.AccountNumber == 1000000007).FirstOrDefault();
            var t7 = new Transaction()
            {
                Description = "Initial Transaction",
                TransactionType = TransactionType.deposit,
                Amount = 7916.4m,
                Date = DateTime.Parse("1/1/2016"),
                TransactionNumber = 1,
                Owner = db.Users.FirstOrDefault(c => c.Email == "westj@pioneer.net"),
                Savings = new List<Saving>(),
                Checkings = new List<Checking>()
            };
            t7.Savings.Add(account7);
            db.Transactions.AddOrUpdate(t => t.TransactionID, t7);
            db.SaveChanges();

            //create new initial transaction
            var t8 = new Transaction()
            {
                Description = "Initial Transaction",
                TransactionType = TransactionType.deposit,
                Amount = 0,
                Date = DateTime.Parse("1/1/2016"),
                TransactionNumber = 1,
                Owner = db.Users.FirstOrDefault(c => c.Email == "mb@aool.com"),
                Stock = db.Stocks.FirstOrDefault(c => c.AccountNumber == 1000000008)
            };

            db.Transactions.AddOrUpdate(t => t.TransactionID, t8);
            db.SaveChanges();

            //create new initial transaction
            var account9 = db.Savings.Where(c => c.AccountNumber == 1000000009).FirstOrDefault();
            var t9 = new Transaction()
            {
                Description = "Initial Transaction",
                TransactionType = TransactionType.deposit,
                Amount = 69576.83m,
                Date = DateTime.Parse("1/1/2016"),
                TransactionNumber = 1,
                Owner = db.Users.FirstOrDefault(c => c.Email == "jeff@ggmail.com"),
                Savings = new List<Saving>(),
                Checkings = new List<Checking>()
            };
            t9.Savings.Add(account9);
            db.Transactions.AddOrUpdate(t => t.TransactionID, t9);
            db.SaveChanges();

            //create new initial transaction
            var t10 = new Transaction()
            {
                Description = "Initial Transaction",
                TransactionType = TransactionType.deposit,
                Amount = 0,
                Date = DateTime.Parse("1/1/2016"),
                TransactionNumber = 1,
                Owner = db.Users.FirstOrDefault(c => c.Email == "nelson.Kelly@aool.com"),
                Stock = db.Stocks.FirstOrDefault(c => c.AccountNumber == 1000000010)
            };

            db.Transactions.AddOrUpdate(t => t.TransactionID, t10);
            db.SaveChanges();

            //create new initial transaction
            var account11 = db.Checkings.Where(c => c.AccountNumber == 1000000011).FirstOrDefault();
            var t11 = new Transaction()
            {
                Description = "Initial Transaction",
                TransactionType = TransactionType.deposit,
                Amount = 30279.33m,
                Date = DateTime.Parse("1/1/2016"),
                TransactionNumber = 1,
                Owner = db.Users.FirstOrDefault(c => c.Email == "erynrice@aool.com"),
                Savings = new List<Saving>(),
                Checkings = new List<Checking>()
            };
            t11.Checkings.Add(account11);
            db.Transactions.AddOrUpdate(t => t.TransactionID, t11);
            db.SaveChanges();

            //create new initial transaction
            var t12 = new Transaction()
            {
                Description = "Initial Transaction",
                TransactionType = TransactionType.deposit,
                Amount = 5000,
                Date = DateTime.Parse("1/1/2016"),
                TransactionNumber = 1,
                Owner = db.Users.FirstOrDefault(c => c.Email == "mackcloud@pimpdaddy.com"),
                IRA = db.IRAs.FirstOrDefault(c => c.AccountNumber == 1000000012)
            };

            db.Transactions.AddOrUpdate(t => t.TransactionID, t12);
            db.SaveChanges();

            //create new initial transaction
            var account13 = db.Savings.Where(c => c.AccountNumber == 1000000013).FirstOrDefault();
            var t13 = new Transaction()
            {
                Description = "Initial Transaction",
                TransactionType = TransactionType.deposit,
                Amount = 11958.08m,
                Date = DateTime.Parse("1/1/2016"),
                TransactionNumber = 1,
                Owner = db.Users.FirstOrDefault(c => c.Email == "ss34@ggmail.com"),
                Savings = new List<Saving>(),
                Checkings = new List<Checking>()
            };
            t13.Savings.Add(account13);
            db.Transactions.AddOrUpdate(t => t.TransactionID, t13);
            db.SaveChanges();

            //create new initial transaction
            var account14 = db.Savings.Where(c => c.AccountNumber == 1000000014).FirstOrDefault();
            var t14 = new Transaction()
            {
                Description = "Initial Transaction",
                TransactionType = TransactionType.deposit,
                Amount = 72990.47m,
                Date = DateTime.Parse("1/1/2016"),
                TransactionNumber = 1,
                Owner = db.Users.FirstOrDefault(c => c.Email == "tanner@ggmail.com"),
                Savings = new List<Saving>(),
                Checkings = new List<Checking>()
            };
            t14.Savings.Add(account14);
            db.Transactions.AddOrUpdate(t => t.TransactionID, t14);
            db.SaveChanges();

            //create new initial transaction
            var account15 = db.Savings.Where(c => c.AccountNumber == 1000000015).FirstOrDefault();
            var t15 = new Transaction()
            {
                Description = "Initial Transaction",
                TransactionType = TransactionType.deposit,
                Amount = 7417.2m,
                Date = DateTime.Parse("1/1/2016"),
                TransactionNumber = 1,
                Owner = db.Users.FirstOrDefault(c => c.Email == "liz@ggmail.com"),
                Savings = new List<Saving>(),
                Checkings = new List<Checking>()
            };
            t15.Savings.Add(account15);
            db.Transactions.AddOrUpdate(t => t.TransactionID, t15);
            db.SaveChanges();

            //create new initial transaction
            var t16 = new Transaction()
            {
                Description = "Initial Transaction",
                TransactionType = TransactionType.deposit,
                Amount = 5000,
                Date = DateTime.Parse("1/1/2016"),
                TransactionNumber = 1,
                Owner = db.Users.FirstOrDefault(c => c.Email == "ra@aoo.com"),
                IRA = db.IRAs.FirstOrDefault(c => c.AccountNumber == 1000000016)
            };

            db.Transactions.AddOrUpdate(t => t.TransactionID, t16);
            db.SaveChanges();

            //create new initial transaction
            var t17 = new Transaction()
            {
                Description = "Initial Transaction",
                TransactionType = TransactionType.deposit,
                Amount = 0,
                Date = DateTime.Parse("1/1/2016"),
                TransactionNumber = 1,
                Owner = db.Users.FirstOrDefault(c => c.Email == "johnsmith187@aool.com"),
                Stock = db.Stocks.FirstOrDefault(c => c.AccountNumber == 1000000017)
            };

            db.Transactions.AddOrUpdate(t => t.TransactionID, t17);
            db.SaveChanges();

            //create new initial transaction
            var account18 = db.Savings.Where(c => c.AccountNumber == 1000000018).FirstOrDefault();
            var t18 = new Transaction()
            {
                Description = "Initial Transaction",
                TransactionType = TransactionType.deposit,
                Amount = 1642.82m,
                Date = DateTime.Parse("1/1/2016"),
                TransactionNumber = 1,
                Owner = db.Users.FirstOrDefault(c => c.Email == "mclarence@aool.com"),
                Savings = new List<Saving>(),
                Checkings = new List<Checking>()
            };
            t18.Savings.Add(account18);
            db.Transactions.AddOrUpdate(t => t.TransactionID, t18);
            db.SaveChanges();

            //create new initial transaction
            var account19 = db.Checkings.Where(c => c.AccountNumber == 1000000019).FirstOrDefault();
            var t19 = new Transaction()
            {
                Description = "Initial Transaction",
                TransactionType = TransactionType.deposit,
                Amount = 84421.45m,
                Date = DateTime.Parse("1/1/2016"),
                TransactionNumber = 1,
                Owner = db.Users.FirstOrDefault(c => c.Email == "ss34@ggmail.com"),
                Savings = new List<Saving>(),
                Checkings = new List<Checking>()
            };
            t19.Checkings.Add(account19);
            db.Transactions.AddOrUpdate(t => t.TransactionID, t19);
            db.SaveChanges();
        }

    }
}


