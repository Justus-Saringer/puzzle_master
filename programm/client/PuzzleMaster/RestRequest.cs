using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using Point = System.Drawing.Point;
using Image = System.Drawing.Image;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Web;
using System.Net.Http.Headers;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Diagnostics;
using System.Net.Http.Formatting;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace PuzzleMaster
{
    class RestRequest
    {
        private HttpClient client = new HttpClient();

        public RestRequest()
        {
            Configuration config = new Configuration();
            client.BaseAddress = new Uri(config.location);
        }

        // Fragt den Server, alle kleinen Bilder zu schicken
        public List<Picture> pictureAll()
        {
            // Man bestimmt den Aufbau des Headers (den Aufbau der Anfrage z.B. Daten werden in json oder in xml gesendet)
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            // Das ist die Anfrage an den Server sowie die Speicherung der Antwort in response
            HttpResponseMessage response = client.GetAsync("/picture/get/all").Result;

            // Es wird überprüft ob die Anfrage erfolgreich war
            if (response.IsSuccessStatusCode)
            {
                // Schreibt die Antwort, also die angeforderten Daten, in dataObjects.
                object dataObjects = response.Content.ReadAsAsync<List<Picture>>().Result;
                // Gibt die übermittelten Daten zurück
                return (List<Picture>)dataObjects;
            }
            else
            {
                MessageBox.Show("Something went wrong, please try again");
                throw new HttpRequestException();
            }
        }

        // fragt nach einem spezifischen Bild
        public Picture getPicture(uint id)
        {
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"get/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                object dataObjects = response.Content.ReadAsAsync<Picture>().Result;
                return (Picture)dataObjects;
            }
            else
            {
                Picture Failure = new Picture();
                return Failure;
            }
        }

        // fügt einen Highscoreeintrag der Datenbankliste hinzu
        public void highscoreAdd(uint moves, string name, uint size)
        {
            string query;
            using (var content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]{
            new KeyValuePair<string, string>("name", name),
            new KeyValuePair<string, string>("moveCount", moves + ""),
            new KeyValuePair<string, string>("fieldSize", size + ""),
            }))
            {
                query = content.ReadAsStringAsync().Result;
            }

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("/highscore/add?" + query).Result;
            Debug.WriteLine(query);
        }

        //Auf Anfrage erhält man alle Bestenlisten zurürck
        public List<HighscoreEntry> highscoreAll()
        {
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("/highscore/get/all").Result;
            if (response.IsSuccessStatusCode)
            {
                object dataObjects = response.Content.ReadAsAsync<List<HighscoreEntry>>().Result; // object könnte mit List<HighscoreEntry> ersetzt werden
                return (List<HighscoreEntry>)dataObjects;
            }
            else
            {   
                List<HighscoreEntry> Emptylist = new List<HighscoreEntry>();
                return Emptylist;
            }
        }

        // gibt spezifische Bestenliste der entsprechenden Feldgröße zurück
        public List<HighscoreEntry> highscoreGetAllFromFieldSize(uint size)
        {
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"/highscore/get/{size}").Result;

            if (response.IsSuccessStatusCode)
            {
                object dataObjects = response.Content.ReadAsAsync<List<HighscoreEntry>>().Result;
                return (List<HighscoreEntry>)dataObjects;
            }
            else
            {   // Falls die Liste noch leer ist und es zu keiner Exception kommt
                List<HighscoreEntry> Failure = new List<HighscoreEntry>();
                return Failure;
            }
        }

        // gibt das gefragte Bild zerteilt mit freiem Feld und entsprechender Größe zurück
        public PuzzlePiece[,] puzzleGet(uint size, uint id, Point freefield)
        {
            string query;
            using (var content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]{
            new KeyValuePair<string, string>("size", size + ""),
            new KeyValuePair<string, string>("pictureId", id + ""),
            new KeyValuePair<string, string>("x", freefield.X + ""),
            new KeyValuePair<string, string>("y", freefield.Y + ""),
            }))
            {
                query = content.ReadAsStringAsync().Result;
            }

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"/puzzle/get?" + query).Result;
            // Hier kommt ne NullReferenz das dataObjects ist null -->
            // und was ist wenn ich einfach die Größe nehme die als übergabeVariable, die in die Funktion kommt, nehme ? @Jutus von Peter
            object dataObjects = response.Content.ReadAsAsync<PuzzlePiece[][]>().Result;
            int sendedArrayLength = ((PuzzlePiece[][])dataObjects).Length;
            //int sendedArrayLength = (int)size;
            if (response.IsSuccessStatusCode)
            {
                PuzzlePiece[,] getArray = new PuzzlePiece[sendedArrayLength, sendedArrayLength];

                // Arraykonverter von PuzzlePiece[][] in PuzzlePiece[,]
                for (int ArrayIndexRow = 0; ArrayIndexRow < sendedArrayLength; ArrayIndexRow++)
                {
                    for (int ArrayIndexColumn = 0; ArrayIndexColumn < sendedArrayLength; ArrayIndexColumn++)
                    {
                        getArray[ArrayIndexRow, ArrayIndexColumn] = ((PuzzlePiece[][])dataObjects)[ArrayIndexRow][ArrayIndexColumn];
                    }
                }
                return getArray;

                // neuer Arraykonverter da der alte Problem macht

            }
            else
            {
                // Wenn ein Fehler auftritt
                PuzzlePiece[,] getArray = new PuzzlePiece[0, 0];
                return getArray;
            }
        }
    }
}
// Test-Durchgang
//foreach (var item in (List<HighscoreEntry>)dataObjects)
//{
//    Debug.WriteLine("{0}", item.name);
//    Debug.WriteLine("{0}", item.moves);
//    Debug.WriteLine("{0}", item.size);
//}