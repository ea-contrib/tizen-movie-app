import { User } from '../../models';

export const performLogin = () : Promise<User> => {
    const userData = new Promise<User>((resolve, reject) => {
        setTimeout(() => {
            resolve({ id: '1', name: 'Developer' });
        }, 250);
    });
    return userData;
};

export const performLogout = () : Promise<void> => {
    return new Promise<void>((resolve, reject) => {
        setTimeout(() => {
            resolve();
        }, 250);
    });
};