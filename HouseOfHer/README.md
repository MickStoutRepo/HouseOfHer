- Informatie over Calendar: https://www.youtube.com/watch?v=oSeYvMEH7jc&t=2922s (2:23:00) <br>

________________________________________________________________________
# Het lezen van json:<br>
<code>private void FunctieNaam()<br>
{<br>
    string json = File.ReadAllText(pad/naar/file.json);<br>
    DataListNaam = JsonConvert.DeserializeObject<List<NaamObject>>(json);<br>
}<br>
</code><br><br>
Dit kun je gebruiken om een json file uit te lezen en een variabel aan te maken die de inhoud als lijst op slaat. 
Hiervoor moet het wel een array zijn met objecten. Gebruikte structuur is:<br>
<br>
<code>
[<br>
    {<br>
        "objectnaam": {<br>
            "onderdeelnaam": "onderdeel inhoud",<br>
            "onderdeelnaam": "onderdeel inhoud"<br>
            }<br>
    }<br>
]<br>
</code>
________________________________________________________________________
# Het ophalen van data uit Calendar

<code>private void FunctieNaam<T>()<br>
{<br>
NaamTextBlockHeader.Text = selectedDate.ToString("d");<br><br>
var item = dataList.FirstOrDefault(d => dateSelector(d).Date == selectedDate.Date);<br>
if (item != null)<br>
{<br>
var descriptions = descriptionSelector(item)<br>
.Where(d => !string.IsNullOrEmpty(d.Value))<br>
.Select(d => $"{d.Key}: {d.Value}");<br>
<br>
NaamTextBlock.Text = string.Join("\n", descriptions);<br>
}<br>
else<br>
{<br>
NaamTextBlock.Text = "Standaard bericht als datum geen inhoud heeft";<br>
}<br>
}</code><br>
<br>
"d" = standaard datum notatie (d.Value = dus 31/03/2025)<br>
________________________________________________________________________
# Opslaan in json file
<code>private void FunctieNaam<T>(List<T> dataList, string filePath)<br>
{<br>
string json = JsonConvert.SerializeObject(dataList, Formatting.Indented);<br>
<br>
File.WriteAllText(pad/naar/file.json, json);<br>
}</code><br>
________________________________________________________________________
# Public class 
<code>public class KlasseNaam<br>
{<br>
public DateTime Date { get; set; }<br>
public Dictionary<string, string> Description { get; set; }<br>
<br>
// Constructor van variabelen //<br>
public Appointment(DateTime date, Dictionary<string, string> description)<br>
{<br>
Date = date;<br>
Description = description;<br>
}<br>
public void AddDescription(string key, string value)<br>
</code>
<br>
Onderdelen:<br>
<br>
<b>Date:</b><br><br>
<b>Type:</b> DateTime<br>
Dit is een eigenschap die de datum en tijd van de afspraak opslaat.<br>
public DateTime Date { get; set; } betekent dat deze eigenschap publiek toegankelijk is en zowel gelezen als geschreven kan worden.<br><br>
<b>Description:</b><br><br>
<b>Type</b>: Dictionary<string, string><br>
Dit is een eigenschap die beschrijvingen van de afspraak opslaat in een woordenboek (dictionary) met sleutels en waarden van het type string.<br>
public Dictionary<string, string> Description { get; set; } betekent dat deze eigenschap ook publiek toegankelijk is en zowel gelezen als geschreven kan worden.<br>
________________________________________________________________________