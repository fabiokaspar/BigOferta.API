// site https://www.json-generator.com/
[
  '{{repeat(5)}}',
  {
    Name: '{{firstName("male")}} {{surname()}}',
    UserName: function (tags, index) { 
      return this.Name.split(' ')[0]; 
    },
    DateOfBirth: '{{date(new Date(1950,0,1), new Date(1999, 11, 31), "YYYY-MM-dd")}}',
    Created: '{{date(new Date(2000,0,1), new Date(2019, 0, 1), "YYYY-MM-dd")}}',
    City: '{{city()}}',
    Country: '{{country()}}',
    ProfilePhoto: 
    {
        url: function(num) {
        		return 'https://randomuser.me/api/portraits/men/' + num.integer(1,99) + '.jpg';
        },
        isMain: true,
        description: '{{lorem()}}'
  	}
    
  }
]

// site https://www.json-generator.com/
[
    '{{repeat(5)}}',
    {
      Name: '{{firstName("female")}} {{surname()}}',
      UserName: function (tags, index) { 
        return this.Name.split(' ')[0]; 
      },
      DateOfBirth: '{{date(new Date(1950,0,1), new Date(1999, 11, 31), "YYYY-MM-dd")}}',
      Created: '{{date(new Date(2017,0,1), new Date(2017, 7, 31), "YYYY-MM-dd")}}',
      
      City: '{{city()}}',
      Country: '{{country()}}',
      ProfilePhoto: 
      {
          url: function(num) {
                  return 'https://randomuser.me/api/portraits/women/' + num.integer(1,99) + '.jpg';
          },
          isMain: true,
          description: '{{lorem()}}'
        }
      
    }
]

// OFFERS
/************************************************************************************/
[
  {
    "Category": "diversão",
    "Title": "Officia cupidatat ad aute nulla ex sint proident anim esse consectetur velit amet culpa sit.",
    "Description": "Pariatur ullamco eiusmod incididunt cillum amet fugiat sit aute mollit aute eiusmod. Velit consectetur dolor qui eiusmod in elit excepteur.",
    "Advertiser": "veniam",
    "Price": 82.77,
    "IsHanked": true,
    "Album": [
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+4",
        "isMain": false
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+30",
        "isMain": false
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+84",
        "isMain": true
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+83",
        "isMain": false
      }
    ]
  },
  {
    "Category": "restaurantes",
    "Title": "Ut est commodo sit sunt Lorem amet sint eu labore fugiat aliquip magna.",
    "Description": "Exercitation duis culpa voluptate veniam in qui reprehenderit ad nulla nulla. Amet non cillum laborum ipsum.",
    "Advertiser": "eu",
    "Price": 58.86,
    "IsHanked": true,
    "Album": [
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+11",
        "isMain": false
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+88",
        "isMain": false
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+15",
        "isMain": false
      }
    ]
  },
  {
    "Category": "restaurantes",
    "Title": "Proident excepteur laborum aliquip veniam consectetur occaecat consectetur sit reprehenderit Lorem cupidatat fugiat laborum non.",
    "Description": "Veniam officia sint est ullamco velit minim ex proident laborum ipsum. Dolore magna mollit dolore minim ad in dolore incididunt qui consectetur.",
    "Advertiser": "ad",
    "Price": 91.51,
    "IsHanked": true,
    "Album": [
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+64",
        "isMain": false
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+8",
        "isMain": true
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+18",
        "isMain": true
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+15",
        "isMain": false
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+40",
        "isMain": true
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+9",
        "isMain": false
      }
    ]
  },
  {
    "Category": "restaurantes",
    "Title": "Officia velit sit commodo mollit quis aliqua excepteur duis culpa duis nostrud ea amet.",
    "Description": "Nulla ex dolor tempor et voluptate Lorem magna consequat aliquip cupidatat laboris ea ad nostrud. Ea ut ea cupidatat ex ut mollit sint irure elit pariatur consectetur nulla nulla reprehenderit.",
    "Advertiser": "fugiat",
    "Price": 80.93,
    "IsHanked": false,
    "Album": [
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+2",
        "isMain": true
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+58",
        "isMain": true
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+81",
        "isMain": false
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+53",
        "isMain": true
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+32",
        "isMain": false
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+89",
        "isMain": false
      }
    ]
  },
  {
    "Category": "diversão",
    "Title": "Cupidatat laboris id exercitation ex quis aute dolor est minim enim non.",
    "Description": "Aute culpa cillum veniam aliqua est nisi do consequat adipisicing et non mollit. Duis id deserunt laborum elit aliquip anim id nulla.",
    "Advertiser": "sunt",
    "Price": 16.91,
    "IsHanked": true,
    "Album": [
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+93",
        "isMain": true
      }
    ]
  },
  {
    "Category": "restaurantes",
    "Title": "Eiusmod aute nulla deserunt duis cupidatat incididunt nostrud nisi laboris ea voluptate non.",
    "Description": "Sit aute elit sint pariatur culpa commodo culpa qui duis est adipisicing. Labore amet pariatur incididunt minim aliquip laborum.",
    "Advertiser": "occaecat",
    "Price": 11.93,
    "IsHanked": false,
    "Album": [
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+79",
        "isMain": false
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+33",
        "isMain": false
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+62",
        "isMain": true
      }
    ]
  },
  {
    "Category": "diversão",
    "Title": "Aliqua sunt amet ad irure enim anim occaecat ullamco reprehenderit Lorem irure tempor ullamco sit.",
    "Description": "Amet laboris sint velit esse. Velit amet cillum commodo anim ex ad sunt elit pariatur consectetur nostrud aliqua.",
    "Advertiser": "cupidatat",
    "Price": 68.36,
    "IsHanked": false,
    "Album": [
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+70",
        "isMain": true
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+80",
        "isMain": true
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+98",
        "isMain": false
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+69",
        "isMain": true
      }
    ]
  },
  {
    "Category": "restaurantes",
    "Title": "Cupidatat elit nostrud eu cupidatat enim.",
    "Description": "Ullamco ut laboris excepteur ex sint anim culpa eu voluptate. Aliquip quis nulla voluptate labore aliqua dolor non consectetur dolor irure.",
    "Advertiser": "ut",
    "Price": 20.93,
    "IsHanked": false,
    "Album": [
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+40",
        "isMain": false
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+14",
        "isMain": true
      }
    ]
  },
  {
    "Category": "diversão",
    "Title": "Qui magna esse labore amet minim tempor ad aute.",
    "Description": "Magna cillum labore commodo dolore. Anim qui voluptate velit eiusmod laborum aliqua nisi aute sit Lorem aliquip cupidatat.",
    "Advertiser": "in",
    "Price": 74.3,
    "IsHanked": true,
    "Album": [
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+47",
        "isMain": true
      }
    ]
  },
  {
    "Category": "restaurantes",
    "Title": "Sunt amet do veniam amet ipsum enim sint eu labore minim pariatur.",
    "Description": "Officia duis dolor eu irure excepteur velit. Veniam consequat irure aliqua ullamco elit adipisicing irure Lorem amet tempor nostrud in.",
    "Advertiser": "officia",
    "Price": 61.62,
    "IsHanked": true,
    "Album": [
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+38",
        "isMain": false
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+21",
        "isMain": false
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+45",
        "isMain": false
      },
      {
        "url": "https://via.placeholder.com/200x100/555555/FFFFFF?text=BIG+OFERTA+17",
        "isMain": false
      }
    ]
  }
]