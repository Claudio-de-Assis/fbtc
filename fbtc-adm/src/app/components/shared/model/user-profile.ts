import { Pessoa } from './pessoa';

export class UserProfile extends Pessoa {
    passwordHashReturned: string;
}

export class UserProfileLogin {
    eMail: string;
    passwordHash: string;
}
