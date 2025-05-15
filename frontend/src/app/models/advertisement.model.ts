

export interface Advertisement {
    id: number;
    name: string;
    price: number;
    description: string;
    postedBy: User;
    imageUrl: string;
    startsOn: string; // ISO date string
    endsOn: string;   // ISO date string
    cityArea: string;
    AdvertisementImages: Image[];
    AdvertisementFeatures: Features[];
    category: string;
    categoryId: number;
    userId: number;
  }

export interface User {
    id: number;
    name: string;
    email: string;
    loginId: string;
    password: string;
    city: string;
    securityQuestion: string | null;
    securityAnswer: string | null;
    birthDate: string | null; 
    contactNumber: string;
    userImage: string | null;
    PhoneNumber: string;

  }

  export interface Category {
    id: number;
    name: string;
    image: URL;
    advertisementCount : number;
    advertisements: Advertisement[];
  }
  
  export interface CityArea {
    id: number;
    name: string;
    user: User;
  }
  
  export interface Image {
    id: number;
    rank: number;
    imagePath:   URL;
  }
  
  export interface Features {
    id: number;
    name: string;
    value: string;
  }
  