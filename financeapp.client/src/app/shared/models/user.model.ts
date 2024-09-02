export class User {
  email: string;
  password: string;

  constructor(model: any) {
    this.email = model.email;
    this.password = model.password;
  }
}
