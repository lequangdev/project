import { Cloudinary } from '@cloudinary/url-gen';
import axios from 'axios';

// Khởi tạo Cloudinary với Cloud Name
const cloudinary = new Cloudinary({
  cloud: {
    cloudName: 'dvyqx6mfk', // cloudName từ web Cloudinary Dashboard
    api_key: '394453755764983',      // API key phải lấy từ Dashboard
    api_secret: 'Y1MxHw4eB8ssvFreIKtVyXDVhqc'  // API secret phải lấy từ Dashboard
  },
  url: {
    secure: true, // Sử dụng HTTPS
  },
});

// Hàm upload ảnh lên Cloudinary
export async function uploadImage(files) {
    let imageUrls = [];
    const formData = new FormData();
    const folderName = "upload_img";
    formData.append('upload_preset', 'upload-img'); // Thay bằng Upload Preset của bạn
    formData.append('folder', folderName);
    try {
      for (const file of files) 
      {
        formData.append('file', file);
        await axios
        .post('https://api.cloudinary.com/v1_1/dvyqx6mfk/image/upload',
          formData,
          {
            headers: {
              'Content-Type': 'multipart/form-data',
            }
          }
        )
        .then(response => {
            imageUrls.push(response.data.secure_url);
        })
      }
      return imageUrls;
    } catch (error) {
      console.error('Error uploading image:', error);
      throw error;
    }
}
export default cloudinary;
