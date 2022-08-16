using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tarea3_1MV2.Firebase
{
    public class Conexion
    {
        public static FirebaseClient firebase = new FirebaseClient("https://xamarin3-2mv2-default-rtdb.firebaseio.com/");
    }
}
