var host = 'https://freeresumebuilder.azurewebsites.net';

document.getElementById('dataForm').addEventListener('submit', function(event) {
  event.preventDefault();

  const Name = document.getElementById('name').value;
  const Email = document.getElementById('email').value;

  const data = {
    "Name": Name,
    "Base64avatar": "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mNk+A8AAQUBAScY42YAAAAASUVORK5CYII=", // Placeholder, you may replace it with an actual base64 string
    "ThemeColor": "#FFFFFF",
    "Profession": "Software Engineer",
    "Address": "123 Main St, City",
    "Phone": "123-456-7890",
    "Email": Email,
    "Linkedin": "https://www.linkedin.com/in/johndoe",
    "Github": "https://github.com/johndoe",
    "Summary": "Experienced software engineer with a passion for coding.",
    "SkillList": ["Java", "Python", "C#"],
    "LanguageList": [
      { "Name": "English", "Level": "Fluent" },
      { "Name": "Spanish", "Level": "Intermediate" }
    ],
    "WorkExperienceList": [
      {
        "Company": "ABC Inc",
        "Role": "Software Developer",
        "StartDate": "2018-01-01",
        "EndDate": "2022-12-31",
        "Description": "Worked on various projects."
      },
      {
        "Company": "XYZ Corp",
        "Role": "Senior Developer",
        "StartDate": "2023-01-01",
        "EndDate": null,
        "Description": "Leading a team of developers."
      }
    ],
    "EducationList": [
      {
        "Year": "2014-2018",
        "Institution": "University of XYZ",
        "Title": "Bachelor of Science in Computer Science",
        "Description": "GPA: 3.8/4.0"
      },
      {
        "Year": "2009-2013",
        "Institution": "High School ABC",
        "Title": "High School Diploma"
      }
    ]
  };

  fetch(host + '/Doc/Preview', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(data)
  })
  .then(response => response.json())
  .then(data => {
    console.log(data);

    const elem = document.getElementById('imgPreview');
    if (elem) {
      elem.src = 'data:image/png;base64,' + data[0];
    } else {
      const previewImg = document.createElement('img');
      previewImg.id = 'imgPreview';
      previewImg.src = 'data:image/png;base64,' + data[0];
      previewImg.height = window.screen.height * 0.6;
      document.body.appendChild(previewImg);
    }

    document.getElementById('responseMessage').innerText = 'Data submitted successfully!';
  })
  .catch((error) => {
    console.error('Error:', error);
    document.getElementById('responseMessage').innerText = 'An error occurred. Please try again.';
  });
});

function previewPdf(color) {
  var uri = host + '/Doc/ExamplePreview?color=' + encodeURIComponent(color);
  fetch(uri)
    .then(response => response.json())
    .then(data => {
      const elem = document.getElementById('imgPreview');
      if (elem) {
        elem.src = 'data:image/png;base64,' + data[0];
      } else {
        const previewImg = document.createElement('img');
        previewImg.id = 'imgPreview';
        previewImg.src = 'data:image/png;base64,' + data[0];
        previewImg.height = window.screen.height * 0.6;
        document.body.appendChild(previewImg);
      }
    })
    .catch(err => {
      alert(err);
    });
}