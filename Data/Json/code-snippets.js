// site https://www.json-generator.com/


/* ************************************* AFTER REFACTORING *********************************************** */

// USERS FEMALE
[
  '{{repeat(10)}}',
  {
    Name: '{{firstName("female")}} {{surname()}}',
    UserName: function (tags, index) { 
      return this.Name.split(' ')[0]; 
    },
    DateOfBirth: '{{date(new Date(1950,0,1), new Date(1999, 11, 31), "YYYY-MM-dd")}}',
    Created: '{{date(new Date(2000,0,1), new Date(2019, 0, 1), "YYYY-MM-dd")}}',
    City: '{{city()}}',
    Country: '{{country()}}',
    Email: '{{email()}}',
    PhoneNumber: '{{phone()}}',
    ProfilePhoto: 
    {
        Url: function(num) {
        		return 'https://randomuser.me/api/portraits/women/' + num.integer(1,99) + '.jpg';
        },
        IsMain: true
  	},
    Street: '{{street()}}',
    State: '{{state()}}',
    District: '{{city()}} district',
    Number: '{{integer(1, 100)}}'
  }
]

// USERS MALE

[
  '{{repeat(10)}}',
  {
    Name: '{{firstName("male")}} {{surname()}}',
    UserName: function (tags, index) { 
      return this.Name.split(' ')[0]; 
    },
    DateOfBirth: '{{date(new Date(1950,0,1), new Date(1999, 11, 31), "YYYY-MM-dd")}}',
    Created: '{{date(new Date(2000,0,1), new Date(2019, 0, 1), "YYYY-MM-dd")}}',
    City: '{{city()}}',
    Country: '{{country()}}',
    Email: '{{email()}}',
    PhoneNumber: '{{phone()}}',
    ProfilePhoto: 
    {
        Url: function(num) {
        		return 'https://randomuser.me/api/portraits/men/' + num.integer(1,99) + '.jpg';
        },
        IsMain: true
  	},
    Street: '{{street()}}',
    State: '{{state()}}',
    District: '{{city()}} district',
    Number: '{{integer(1, 100)}}'
  }
]

// OFFERS

[
  '{{repeat(10)}}',
  {
    
    Category: function (tags) {
      var cats = ['restaurantes', 'diversão'];
      return cats[tags.integer(0, cats.length - 1)];
    },
    
    Title: '{{lorem(integer(2, 4), "words")}}',
    Description: '{{lorem()}}',
    Advertiser: '{{lorem(integer(1, 2), "words")}}',
    Price: '{{floating(10, 100, 2)}}',
    IsHanked: '{{bool()}}',
    ComoUsar: function (tags) {
      var dicas = ["Não é necessário agendar",
                   "Válido de segunda a quinta das 12h as 23h",
                   "Necessário agendar com no mínimo 48 horas de antecedência",
                    "Válido de segunda a sábado das 12h as 23h. Necessário agendamento prévio",
                   "Não é necessário agendar. Sujeito à fila."
                   
                  ];
      return dicas[tags.integer(0, dicas.length - 1)];
    },
    OndeFica: function (tags) {
      var locais =	[
        				"Avenida João da Silva, 255, São Paulo - SP",
						"Rua Francisco Mendes, 1000, Rio de Janeiro - RJ",
        				"Avenida Lúcio Rodrigues Alves, 33, Fortaleza - CE",
        				"Avenida José de Oliveira, 550, Campo Grande - MS",
        				"Avenida Júlia Abrão, 77, São Paulo - SP",
        				"Estrada Maria das Graças, 12, Salvador - BA"
      				];
      return locais[tags.integer(0, locais.length - 1)];
    },
    Photos: [
      '{{repeat(integer(1, 4))}}',
      {
        Url: function(tag) 
        {
          return 'https://via.placeholder.com/400x200/aaa/FFFFFF/?text=BIGOFERTA+'+tag.integer(1, 50);
        },
        IsMain: '{{bool()}}'
      }
    ]
  }
]