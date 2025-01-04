import { jwtDecode } from 'jwt-decode';
function decodeJWT(token) {
        const decoded = jwtDecode(token);
        return decoded;
}
export default decodeJWT
    
