using Core.Entities.Conceretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarNameIsNotValid = "Car name is not valid !";
        public static string CarDailyPriceIsNotValid = "Car's daily price is not valid !";

        public static string RentalCarIsNotReturn = "The rental car has not been returned yet!!";

        public static string RentalDayIsNotBeforeNow = "Rental date cannot be earlier than now!";

        public static string ReturnDayIsNotBeforeRentalDay = "Return day cannot be earlier than rental day!";

        public static string RentalDayIsNotParsing = "Rental day is not parsing.";
        public static string ReturnDayIsNotParsing = "Return day is not parsing.";

        public static string RentalCarIsPassRentDay = "Rental car rent day is pass!";
        public static string RentalCarIsPassReturnDay = "Rental car return day is pass!";

        public static string CarImagesLimitExceded = "Car images limit exceded!";

        public static string UserAlreadyExists = "User Already Exists!";
        public static string UserIsNotFound = "User Is Not Found!";

        public static string SuccessfulRegister = "Successful Registered!";
        public static string SuccessfulLogin = "Successful Login!";

        public static string AuthorizationDenied = "Authrorization Denied!!";

        public static string PasswordError = "Password is not correct!";
    }
}