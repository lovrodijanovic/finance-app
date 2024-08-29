export class User {
  age: number;
  email: string;
  password: string;

  constructor(model: any) {
    this.age = model.age;
    this.email = model.email;
    this.password = model.password;
  }
}
