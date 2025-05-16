using WebAPI.Entities;

namespace WebAPI.Database;

public static class DatabaseSeeder
{
    public static async void Seed(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.EnsureCreatedAsync();
        if (!context.Scopes.Any())
        {
            var scopes = new List<Scope>
            {
                new Scope { Value = "admin" },
                new Scope { Value = "write:content" },
                new Scope { Value = "read:user" }
            };
            await context.Scopes.AddRangeAsync(scopes);
            await context.SaveChangesAsync();
        }

        if (!context.Users.Any())
        {
            var user = new User
            {
                Email = "admin@k2systems.pl",
                FirstName = "admin",
                LastName = "admin",
                PasswordHash = "$2a$11$LZCEiWBMqqPxpuP4TzG07OjYw97XMLcgGdHsShSZZW5jvU3uERxMO",
                Scopes = context.Scopes.ToList()
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        if (!context.EditableContents.Any())
        {
            var admin = context.Users.First();
            var contents = new List<EditableContent>
            {
                // LandingPage
                new EditableContent { LastEditor = admin, Content = "LandingPage:TitleLandingPage", Key = "TitleLandingPage", Page = "LandingPage", Language = "en" },

                // Footer
                new EditableContent { LastEditor = admin, Content = "Footer:TitleFooterPage", Key = "TitleFooterPage", Page = "Footer", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "Footer:TitleContact", Key = "TitleContact", Page = "Footer", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "Footer:TitleOffers", Key = "TitleOffers", Page = "Footer", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "Footer:GmailContact", Key = "GmailContact", Page = "Footer", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "Footer:LocationContact", Key = "LocationContact", Page = "Footer", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "Footer:PhoneNumberContact", Key = "PhoneNumberContact", Page = "Footer", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "Footer:FirstOfferOffers", Key = "FirstOfferOffers", Page = "Footer", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "Footer:SecondOfferOffers", Key = "SecondOfferOffers", Page = "Footer", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "Footer:ThirdOfferOffers", Key = "ThirdOfferOffers", Page = "Footer", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "Footer:FourthOfferOffers", Key = "FourthOfferOffers", Page = "Footer", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "Footer:FifthOfferOffers", Key = "FifthOfferOffers", Page = "Footer", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "Footer:SixthOfferOffers", Key = "SixthOfferOffers", Page = "Footer", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "Footer:CoopyRight", Key = "CoopyRight", Page = "Footer", Language = "en" },

                // NavBar
                new EditableContent { LastEditor = admin, Content = "NavBar:MainPageLink", Key = "MainPageLink", Page = "NavBar", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "NavBar:DeliverLink", Key = "DeliverLink", Page = "NavBar", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "NavBar:AboutUsLink", Key = "AboutUsLink", Page = "NavBar", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "NavBar:RegisterLink", Key = "RegisterLink", Page = "NavBar", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "NavBar:ServiceLink", Key = "ServiceLink", Page = "NavBar", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "NavBar:ShopLink", Key = "ShopLink", Page = "NavBar", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "NavBar:ContactLink", Key = "ContactLink", Page = "NavBar", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "NavBar:SettingsLink", Key = "SettingsLink", Page = "NavBar", Language = "en" },

                // MainPage
                new EditableContent { LastEditor = admin, Content = "MainPage:GreetingsTitle", Key = "GreetingsTitle", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:GreetingsContent", Key = "GreetingsContent", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:FirstInfoTitle", Key = "FirstInfoTitle", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:FirstInfoContent", Key = "FirstInfoContent", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:ServiceTitle", Key = "ServiceTitle", Page = "MainPage", Language = "en" },

                new EditableContent { LastEditor = admin, Content = "MainPage:ServiceFirstSubtitle", Key = "ServiceFirstSubtitle", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:ServiceFirstContent", Key = "ServiceFirstContent", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:ServiceSecondSubtitle", Key = "ServiceSecondSubtitle", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:ServiceSecondContent", Key = "ServiceSecondContent", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:ServiceThirdSubtitle", Key = "ServiceThirdSubtitle", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:ServiceThirdContent", Key = "ServiceThirdContent", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:ServiceFourthSubtitle", Key = "ServiceFourthSubtitle", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:ServiceFourthContent", Key = "ServiceFourthContent", Page = "MainPage", Language = "en" },

                new EditableContent { LastEditor = admin, Content = "MainPage:SecondInfoTitle", Key = "SecondInfoTitle", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:SecondInfoContent", Key = "SecondInfoContent", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:LocationTitle", Key = "LocationTitle", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:FirstContactTitle", Key = "FirstContactTitle", Page = "MainPage", Language = "en" },

                new EditableContent { LastEditor = admin, Content = "MainPage:FirstEmail", Key = "FirstEmail", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:SecondEmail", Key = "SecondEmail", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:ThirdEmail", Key = "ThirdEmail", Page = "MainPage", Language = "en" },

                new EditableContent { LastEditor = admin, Content = "MainPage:SecondContactTitle", Key = "SecondContactTitle", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:FirstPhoneNumber", Key = "FirstPhoneNumber", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:SecondPhoneNumber", Key = "SecondPhoneNumber", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:ThirdPhoneNumber", Key = "ThirdPhoneNumber", Page = "MainPage", Language = "en" },

                new EditableContent { LastEditor = admin, Content = "MainPage:ThirdContactTitle", Key = "ThirdContactTitle", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:CompanyAdress", Key = "CompanyAdress", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:PrinterSubscriptionTitle", Key = "PrinterSubscriptionTitle", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:PrinterFirstSubscriptionOffer", Key = "PrinterFirstSubscriptionOffer", Page = "MainPage", Language = "en" },

                new EditableContent { LastEditor = admin, Content = "MainPage:PrinterFirstSubscriptionFirstFeature", Key = "PrinterFirstSubscriptionFirstFeature", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:PrinterFirstSubscriptionSecondFeature", Key = "PrinterFirstSubscriptionSecondFeature", Page = "MainPage", Language = "en" },

                new EditableContent { LastEditor = admin, Content = "MainPage:PrinterSecondSubscriptionOffer", Key = "PrinterSecondSubscriptionOffer", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:PrinterSecondSubscriptionFirstFeature", Key = "PrinterSecondSubscriptionFirstFeature", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:PrinterSecondSubscriptionSecondFeature", Key = "PrinterSecondSubscriptionSecondFeature", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:PrinterSecondSubscriptionThirdFeature", Key = "PrinterSecondSubscriptionThirdFeature", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:PrinterSecondSubscriptionFourthFeature", Key = "PrinterSecondSubscriptionFourthFeature", Page = "MainPage", Language = "en" },

                new EditableContent { LastEditor = admin, Content = "MainPage:PrinterThirdSubscriptionOffer", Key = "PrinterThirdSubscriptionOffer", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:PrinterThirdSubscriptionFirstFeature", Key = "PrinterThirdSubscriptionFirstFeature", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:PrinterThirdSubscriptionSecondFeature", Key = "PrinterThirdSubscriptionSecondFeature", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:PrinterThirdSubscriptionThirdFeature", Key = "PrinterThirdSubscriptionThirdFeature", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:PrinterThirdSubscriptionFourthFeature", Key = "PrinterThirdSubscriptionFourthFeature", Page = "MainPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "MainPage:PrinterThirdSubscriptionFifthFeature", Key = "PrinterThirdSubscriptionFifthFeature", Page = "MainPage", Language = "en" },

                new EditableContent { LastEditor = admin, Content = "MainPage:RateTitle", Key = "RateTitle", Page = "MainPage", Language = "en" },

                // RegisterLoginPage
                new EditableContent { LastEditor = admin, Content = "RegisterLoginPage:RegisterTitle", Key = "RegisterTitle", Page = "RegisterLoginPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "RegisterLoginPage:NameInputRegister", Key = "NameInputRegister", Page = "RegisterLoginPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "RegisterLoginPage:SecondNameInputRegister", Key = "SecondNameInputRegister", Page = "RegisterLoginPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "RegisterLoginPage:EmailInputRegister", Key = "EmailInputRegister", Page = "RegisterLoginPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "RegisterLoginPage:PasswordInputRegister", Key = "PasswordInputRegister", Page = "RegisterLoginPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "RegisterLoginPage:CheckPasswordInputRegister", Key = "CheckPasswordInputRegister", Page = "RegisterLoginPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "RegisterLoginPage:RegisterButtonName", Key = "RegisterButtonName", Page = "RegisterLoginPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "RegisterLoginPage:LoginTitle", Key = "LoginTitle", Page = "RegisterLoginPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "RegisterLoginPage:EmailInputLogin", Key = "EmailInputLogin", Page = "RegisterLoginPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "RegisterLoginPage:PasswordInputLogin", Key = "PasswordInputLogin", Page = "RegisterLoginPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "RegisterLoginPage:CheckPasswordInputLogin", Key = "CheckPasswordInputLogin", Page = "RegisterLoginPage", Language = "en" },
                new EditableContent { LastEditor = admin, Content = "RegisterLoginPage:LoginButtonName", Key = "LoginButtonName", Page = "RegisterLoginPage", Language = "en" }
            };

            await context.EditableContents.AddRangeAsync(contents);
            await context.SaveChangesAsync();
        }
    }
}