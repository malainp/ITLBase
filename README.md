# ITLBase

ITLBase is a collection of classes that let you get information about an ITL student like the current courses in its schedule, the courses already taken and its information. 

This library is a PCL (Portable Class Library) written in C# and works in .NET 4.5, Windows 8, Windows Phone 8.1, Windows Phone Silverlight 8, Xamarin.Android, Xamarin.iOS and Xamarin.iOS (Classic).

##Installation

You can download the library source code and compile it by yourself or install the library direct in your project via NuGet Package Manager (Recommended)

> Install-Package MAlainp.ITLBase

##Usage

Once you install the library in your project you maybe need to get some student information. Create a new PrepconParser object with the desired student ID and its password, then call (and await if you want) the async method **ParseHTMLAsync**. This will request the information to the ITL web server, parses it and fill some fields in the object (Student Name field in this case).

```cs
using MAlainp.ITLBase;
…
async void SomeMethod(some params…)
{
  //This url is the “main page” and only display the student name and let the students choice where to go next (Cursando, Kardex or Boleta de calificaciones).
  //The other URLs ends with matcursa.asp and kardex.asp
  var url = “http://www.itlalaguna.edu.mx/2014/servicios/escolares/estatus_alumno/prepcon.asp”;
  PrepconParser pp = new PrepconParser(url, student_ID, password);
  bool parsed = await pp.ParseHTMLAsync();
  someTextBox.Text= pp.StudentName;
  //some code
}
```
##To Do

* Change the way the method *ParseHTMLAsync* works, maybe return the list of courses in the schedule or the list in the courses already taken instead of returning a bool type.

* Create a way to parse student’s information with ID starting in 09 or lower.

##Known bugs

* Students with student ID starting in 09 or lower can’t get its information from the server

##Supporting ITLBase

If you want to add functionality to this little PCL send an email with your information and a little C# program (Not a Hello World) to malain.pera@gmail.com to become a collaborator or fork this repo in your GitHub and when you finish send me a pull request to review your changes and merge it with mine.

###You found a bug?

If you found a bug in the PCL please look in [this page](https://github.com/malainp/ITLBase/issues) for another one similar issue, if you don't find anything open a new issue and write what you found (attach an image if possible).

##Special Thanks 

* Eduardo Reza – For providing help in translation
* Gerardo Chiner – For providing a communication channel to Eduardo Reza. Thanks Chiner!

##Licencing

This program is under the  GNU Lesser General Public License.
