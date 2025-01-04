<template>
  <div class="container_content">
    <!-- Main content -->
    <main class="main-content">
      <div class="header">
        <h1>Employee Management</h1>
          <div class="filter-group">
            <input type="text" placeholder="Tìm kiếm (Tên nhân viên)" v-model="searchName" />
            <select v-model="filterPosition" class="position-filter">
              <option value="all">Tất cả vị trí</option>
              <option value="manager">Manager</option>
              <option value="shipper">Shipper</option>
            </select>
        </div>
        <button @click="openModal" class="add-employee">Thêm nhân viên Mới</button>
      </div>

      <!-- Employee Table -->
      <table class="employee-table">
        <thead>
          <tr>
            <th>Ảnh</th>
            <th>Họ và tên</th>
            <th>Ngày sinh</th>
            <th>Số điện thoại</th>
            <th>Căn cước công dân</th>
            <th>Địa chỉ</th>
            <th>Vị trí</th>
            <th>Hành động</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="employee in employeeList" :key="employee.staff_id">
            <td>
              <img :src="employee.staff_imgUrl" alt="Avatar" width="50" height="50" />
            </td>
            <td>{{ employee.fullname }}</td>
            <td>{{ formatDate(employee.staff_dateOfBirth) }}</td>
            <td>{{ employee.phone_number }}</td>
            <td>{{ employee.staff_identification }}</td>
            <td>
              {{
                employee.address_province +
                "-" +
                employee.address_district +
                "-" +
                employee.address_commune +
                "-" +
                employee.address_village +
                "-" +
                employee.address_apartment
              }}
            </td>
            <td>{{ employee.staff_role }}</td>
            <td>
              <div class="edit" @click="editEmployee(employee)">Sửa</div>
              <div
                class="delete"
                @click="
                  deleteEmployee(
                    employee.staff_id,
                    employee.staff_imgUrl,
                    employee.fullname,
                    employee.address_id,
                    employee.user_id,
                )">Xóa</div>
            </td>
          </tr>
          <!-- More rows as needed -->
        </tbody>
      </table>
    </main>
    <div v-if="isModalOpen" @click="closeModal" class="overlay">
      <div class="modal" @click.stop>
        <h3>{{ isEditMode ? 'Sửa thông tin nhân viên' : 'Thêm nhân viên' }}</h3>
        <div class="avatar_show" v-if="newEmployee.avatar">
          <img :src="newEmployee.avatar" alt="Avatar" width="100" height="100" />
        </div>
        <form>
          <div class="input-row">
            <div class="input-column">
              <label for="employee-name">Họ và tên</label>
              <input
                id="employee-name"
                type="text"
                placeholder="Họ và tên"
                v-model="newEmployee.name"
              />
            </div>
            <div class="input-column">
              <label for="employee-idcard">Căn cước công dân</label>
              <input
                id="employee-idcard"
                type="text"
                placeholder="Căn cước công dân"
                v-model="newEmployee.idCard"
              />
            </div>
          </div>
          <div class="input-row">
            <div class="input-column">
              <label for="employee-birthdate">Ngày sinh</label>
              <input
                id="employee-birthdate"
                type="date"
                v-model="newEmployee.birthDate"
              />
            </div>
            <div class="input-column">
              <label for="employee-address">Địa chỉ</label>
              <input
                id="employee-address"
                type="text"
                placeholder="Tỉnh-xã-làng-Số nhà"
                v-model="newEmployee.address"
              />
            </div>
          </div>
          <div class="input-row">
            <div class="input-column">
              <label for="employee-email">Email</label>
              <input
                id="employee-email"
                type="email"
                placeholder="Email"
                v-model="newEmployee.email"
              />
            </div>
            <div class="input-column">
              <label for="employee-phone">Số điện thoại</label>
              <input
                id="employee-phone"
                type="text"
                placeholder="Số điện thoại"
                v-model="newEmployee.phone"
              />
            </div>
          </div>

          <div v-if="!isEditMode" class="input-row">
            <div class="input-column">
              <label for="employee-username">Tài khoản đăng ký</label>
              <input
                id="employee-username"
                type="text"
                placeholder="Tài khoản đăng nhập"
                v-model="newEmployee.account"
              />
            </div>
            <div class="input-column">
              <label for="employee-password">Mật khẩu</label>
              <input
                id="employee-password"
                type="password"
                placeholder="Mật khẩu"
                v-model="newEmployee.password"
              />
            </div>
          </div>
          <div class="input-row">
            <div class="input-column">
              <label for="employee-position">Vị trí</label>
              <select id="employee-position" v-model="newEmployee.position">
                <option value="" disabled>Chọn vị trí</option>
                <option value="manager" selected>Manager</option>
                <option value="shipper">shipper</option>
              </select>
            </div>
            <div class="input-column">
              <label for="employee-avater">Ảnh thẻ</label>
              <input
                id="employee-avatar"
                type="file"
                accept="image/*"
                @change="handleImageUpload"
              />
            </div>
          </div>
          <div class="modal-actions">
            <button type="button" @click="isEditMode ? updateEmployee($event) : addEmployee($event)" class="save">
              {{ isEditMode ? 'Cập nhật' : 'Lưu' }}
            </button>
            <button type="button" @click="closeModal" class="cancel">Hủy</button>
          </div>
        </form>
        <div v-if="showCussesMessage" class="alert alert-success">
          {{ successMessage }}
        </div>
        <div v-if="showErorrMessage" class="alert alert-error">
          {{ errorMessage }}
        </div>
      </div>
    </div>
    <div v-if="showDeleteConfirm" class="overlay">
      <div class="confirm-modal" @click.stop>
        <div class="confirm-avatar">
          <img :src="imgDelete" alt="Avatar" />
        </div>
        <h3>{{ fullNameDelete }}</h3>
        <p>Bạn có chắc chắn muốn xóa nhân viên này?</p>
        <div class="confirm-actions">
          <button @click="confirmDelete" class="confirm-btn">Xác nhận</button>
          <button @click="cancelDelete" class="cancel-btn">Hủy</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { useRouter } from "vue-router";
import HeaderVue from "@/layouts/HeaderVue.vue";
import { getCurrentInstance, ref, reactive, onMounted, computed, watch } from "vue";
import { useStore } from "vuex";
import axios from "axios";
import { uploadImage } from "@/cloudinary";
import avatarDefault from "@/img-avatar-default/images.png";
export default {
  setup() {
    onMounted(async () => {
      await getEmployeeList(filterPosition.value);
    });
    const router = useRouter();
    let store = useStore();
    const isModalOpen = ref(false);
    const showCussesMessage = ref(false);
    const showErorrMessage = ref(false);
    const employeeList = ref([]);
    const successMessage = ref('');
    const searchName = ref("");
    const errorMessage = ref('');
    const employeeDefault = {
      name: "",
      email: "",
      account: "",
      password: "",
      birthDate: "",
      phone: "",
      idCard: "",
      address: "",
      position: "",
      avatar: avatarDefault,
      fileToUpload: avatarDefault,
    };
    const newEmployee = ref({
      name: "",
      email: "",
      account: "",
      password: "",
      birthDate: "",
      phone: "",
      idCard: "",
      address: "",
      position: "",
      avatar: avatarDefault,
      fileToUpload: avatarDefault,
    });
    const showDeleteConfirm = ref(false);
    const staffToDelete = ref(null);
    const imgDelete = ref(null);
    const fullNameDelete = ref(null);
    const address_idDelete = ref(null);
    const user_idDelete = ref(null);
    const isEditMode = ref(false);
    function openModal() {
      isModalOpen.value = true;
      isEditMode.value = false;
      newEmployee.value = { ...employeeDefault };
    }
    function closeModal() {
      isModalOpen.value = false;
      isEditMode.value = false;
      showCussesMessage.value = false;
      showErorrMessage.value = false;
      newEmployee.value = { ...employeeDefault };
    }
    async function addEmployee(event) {
      event.preventDefault();
      try {
        await onSubmit();
        showCussesMessage.value = true;
        successMessage.value = 'Thêm nhân viên thành công!';
      } catch (error) {
        showErorrMessage.value = true;
        errorMessage.value = 'Thêm nhân viên thất bại!';
        console.error("Upload failed:", error);
      }
    }
    async function onSubmit() {
      try {
        const imageUrl = await uploadImage([newEmployee.value.fileToUpload]);
        newEmployee.value.avatar = imageUrl[0];
        let user = {
          fullName: newEmployee.value.name,
          email: newEmployee.value.email,
          phone_number: newEmployee.value.phone,
          account: newEmployee.value.account,
          passWord: newEmployee.value.password,
          avatar_url: imageUrl[0],
          user_role: newEmployee.value.position,
        };
        if (newEmployee.value.position != "manager") {
          user.user_role = "employee";
        }
        await axios({
          method: "post",
          url: "http://localhost:1234/api/Users/Register",
          data: user,
        })
          .then((response) => {
            add_address(response.data)
          })
          .catch((error) => {});
      } catch (error) {
        console.error("Upload failed:", error);
      }
    }
    async function add_address(user_id) {
      try {
        let AddressUser = {
          address_fullName: newEmployee.value.name,
          address_phoneNumber: newEmployee.value.phone,
          address_province: newEmployee.value.address.split("-")[0],
          address_district: newEmployee.value.address.split("-")[1],
          address_commune: newEmployee.value.address.split("-")[2],
          address_village: newEmployee.value.address.split("-")[3],
          address_apartment: newEmployee.value.address.split("-")[4],
          user_id: user_id,
          address_default: 2,
        };
        await axios({
          method: "Post",
          url: "http://localhost:1234/api/Address/InsertResID",
          data: AddressUser,
        })
          .then((response) => {
            console.log(response.data);
            insertStaff(user_id, response.data);
          })
          .catch((error) => {});
      } catch (error) {
        console.error("Upload failed:", error);
      }
    }

    async function insertStaff(user_id, address_id) {
      try {
        let models = {
          user_id: user_id,
          address_id: address_id,
          staff_role: newEmployee.value.position,
          staff_dateOfBirth: newEmployee.value.birthDate,
          staff_identification: newEmployee.value.idCard,
          staff_imgUrl: newEmployee.value.avatar,
        };
        await axios({
          method: "Post",
          url: "http://localhost:1234/api/Staff",
          data: models,
        })
          .then((response) => {
            getEmployeeList(filterPosition.value)
          })
          .catch((error) => {});
      } catch (error) {
        console.error("Upload failed:", error);
      }
    }
    // Hàm xử lý khi chọn
    function handleImageUpload(event) {
      const file = event.target.files[0];
      if (file) {
        newEmployee.value.avatar = URL.createObjectURL(file);
        newEmployee.value.fileToUpload = file;
      }
    }
    function backPage() {
      window.history.back();
    }

    // xửa lý lấy thông tin nhân sự
    async function getEmployeeList(getType) {
      let path = "GetAllByPosition"
      let param = {
        column: "staff_role",
        column_search: "fullname",
        value: "/",
        search: searchName.value || "/"
      }
      switch(getType){
        case 'all':
          path = searchName.value ? "GetAllByPosition" : "getAllStaff"
          break;
        case 'manager':
          param.value = "manager"
          break;
        case 'shipper':
          param.value = "shipper"
          break;
      }
      await axios({
        method: "get",
        url: `http://localhost:1234/api/Staff/${path}`,
        params: param
      })
        .then((response) => {
          employeeList.value = response.data;
        })
        .catch((error) => {});
    }
    // fomat ngày tháng năm
    function formatDate(dateString) {
      if (!dateString) return "";

      const date = new Date(dateString);
      const day = date.getDate().toString().padStart(2, "0");
      const month = (date.getMonth() + 1).toString().padStart(2, "0");
      const year = date.getFullYear();

      return `${day}/${month}/${year}`;
    }

    // xóa nhân viên
    function deleteEmployee(staff_id, staff_imgUrl, fullName, address_id, user_id) {
      showDeleteConfirm.value = true;
      staffToDelete.value = staff_id;
      imgDelete.value = staff_imgUrl;
      fullNameDelete.value = fullName;
      address_idDelete.value = address_id;
      user_idDelete.value = user_id;
    }

    async function confirmDelete() {
      try {
        let listID = {
            user_id: user_idDelete.value,
            staff_id: staffToDelete.value,
            address_id: address_idDelete.value
        }
        await axios({
          method: "delete",
          url: "http://localhost:1234/api/Staff/DeleteListID",
          data: listID
            
        });

        employeeList.value = employeeList.value.filter(
          (emp) => emp.staff_id !== staffToDelete.value
        );
        showDeleteConfirm.value = false;
        staffToDelete.value = null;
        imgDelete.value = null;
        fullNameDelete.value = null;
      } catch (error) {
        console.error("Lỗi khi xóa nhân viên:", error);
        alert("Có lỗi xảy ra khi xóa nhân viên!");
      }
    }

    function cancelDelete() {
      showDeleteConfirm.value = false;
      staffToDelete.value = null;
      imgDelete.value = null;
      fullNameDelete.value = null;
    }

    // sửa thông tin nhân viên
    function editEmployee(employee) {
      isModalOpen.value = true;
      isEditMode.value = true;
      newEmployee.value = {
        name: employee.fullname,
        email: employee.email,
        account: employee.account,
        password: '',  // Không hiển thị mật khẩu cũ
        birthDate: employee.staff_dateOfBirth,
        phone: employee.phone_number,
        idCard: employee.staff_identification,
        address: `${employee.address_province}-${employee.address_district}-${employee.address_commune}-${employee.address_village}-${employee.address_apartment}`,
        position: employee.staff_role,
        avatar: employee.staff_imgUrl,
        staff_id: employee.staff_id,
        user_id: employee.user_id,
        address_id: employee.address_id,
        fileToUpload: employee.staff_imgUrl
      };
    }

    async function updateEmployee(event) {
      
      event.preventDefault();
      try {
        let imageUrl = newEmployee.value.avatar;
        // Upload ảnh mới nếu có
        if (newEmployee.value.fileToUpload !== newEmployee.value.avatar) {
          imageUrl = (await uploadImage([newEmployee.value.fileToUpload]))[0];
        }
        // Cập nhật thông tin user
        const updateData = {
          user_id: newEmployee.value.user_id,
          fullname: newEmployee.value.name,
          email: newEmployee.value.email,
          phone_number: newEmployee.value.phone,
          user_role: newEmployee.value.position != "manager" ? "employee" : "manager",
          address_id: newEmployee.value.address_id,
          address_fullName: newEmployee.value.name,
          address_phoneNumber: newEmployee.value.phone,
          address_province: newEmployee.value.address.split("-")[0],
          address_district: newEmployee.value.address.split("-")[1],
          address_commune: newEmployee.value.address.split("-")[2],
          address_village: newEmployee.value.address.split("-")[3],
          address_apartment: newEmployee.value.address.split("-")[4],
          address_default: 2,
          staff_id: newEmployee.value.staff_id,
          staff_role: newEmployee.value.position,
          staff_dateOfBirth: newEmployee.value.birthDate,
          staff_identification: newEmployee.value.idCard,
          staff_imgUrl: imageUrl,
        };
        await axios({
          method: "put",
          url: "http://localhost:1234/api/Staff/UpdateStaff",
          data: updateData
        })
        .then((response) => {
          successMessage.value = 'Cập nhật nhân viên thành công!';
          showCussesMessage.value = true;
          getEmployeeList(filterPosition.value)
        })
        .catch((error) => {
          errorMessage.value = 'Cập nhật nhân viên thất bại!';
          showErorrMessage.value = true;
        });
      } catch (error) {
        errorMessage.value = 'Cập nhật nhân viên thất bại!';
        showErorrMessage.value = true;
        console.error("Cập nhật thất bại:", error);
      }
    }
    // lọc nhân viên
    const filterPosition = ref('all');
    watch(filterPosition, async (newValue) => {
      await getEmployeeList(newValue);
    });
    // tìm kiếm nhân viên
    const searchTimeout = ref(null)
    watch(searchName, async (newValue) => {
      if (searchTimeout.value) {
        clearTimeout(searchTimeout.value)
      }
      searchTimeout.value = setTimeout(() => {
        getEmployeeList(filterPosition.value)
      }, 500)
    });
    return {
      // Các thuộc tính điều hướng
      backPage,
      // Các thuộc tính liên quan đến modal
      isModalOpen, isEditMode,
      // Các thuộc tính dữ liệu
      newEmployee, employeeList, filterPosition,
      // Các methods xử lý employee
      openModal, closeModal, addEmployee, editEmployee, updateEmployee, deleteEmployee,
      // Các methods xử lý ảnh
      handleImageUpload,
      // Các thuộc tính và methods xử lý xóa
      imgDelete, fullNameDelete, showDeleteConfirm, confirmDelete, cancelDelete,
      // Các thuộc tính và methods xử lý thông báo
      successMessage, errorMessage, showCussesMessage, showErorrMessage,
      // Các utilities
      formatDate,
      searchName
    };
  },
};
</script>

<style scoped>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  font-family: Arial, sans-serif;
}
.modal select {
  display: block;
  width: 100%;
  padding: 10px;
  margin-bottom: 10px;
  border: 1px solid #ddd;
  border-radius: 3px;
  font-size: 1em;
}
.avatar_show {
  text-align: left;
}
.avatar_show img {
  padding: 10px;
  width: 150px;
  height: 150px;
  background-color: rgb(255, 117, 140);
  border-radius: 5px;
}
select {
  outline: none;
}
.container-content {
  display: flex;
}

.sidebar {
  width: 250px;
  background-color: #2c3e50;
  color: #ecf0f1;
  padding: 20px;
  height: 100vh;
}
.confirm-avatar {
  margin-bottom: 20px;
  display: flex;
  justify-content: center;
}

.confirm-avatar img {
  width: 100px;
  height: 100px;
  border-radius: 50%;
  object-fit: cover;
  border: 3px solid #e74c3c;
  padding: 3px;
}
.sidebar h2 {
  font-size: 1.5em;
  margin-bottom: 20px;
}
input:focus {
  outline: none;
}
.sidebar ul {
  list-style: none;
}

.sidebar li {
  padding: 10px 0;
  cursor: pointer;
  color: #bdc3c7;
}

.sidebar li.active,
.sidebar li:hover {
  color: #ecf0f1;
}

/* Main Content */
.main-content {
  flex: 1;
  padding: 20px;
  background-color: #ecf0f1;
}

.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

.header h1 {
  font-size: 1.8em;
  color: #2c3e50;
}

.header input {
  padding: 10px;
  width: 250px;
  border: 1px solid #bdc3c7;
  border-radius: 5px;
}

.add-employee {
  padding: 10px 20px;
  background-color: #3498db;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  outline: none;
}

.add-employee:hover {
  background-color: #2980b9;
}

/* Employee Table */
.employee-table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 10px;
  font-size: 0.9em; /* Giảm kích thước font */
  table-layout: fixed; /* Cố định chiều rộng các cột */
}

.employee-table th,
.employee-table td {
  padding: 5px; /* Giảm padding để thu gọn */
  text-align: left;
  border: 1px solid #ddd;
  overflow-wrap: break-word; /* Tự động ngắt dòng nếu nội dung quá dài */
}

.employee-table th {
  background-color: #3498db;
  color: white;
  font-weight: bold;
}

/* Định chiều rộng cố định cho các cột */
.employee-table th:nth-child(1),
.employee-table td:nth-child(1) {
  width: 6%;
}
.employee-table th:nth-child(2),
.employee-table td:nth-child(2) {
  width: 14%;
}
.employee-table th:nth-child(3),
.employee-table td:nth-child(3) {
  width: 8%;
}
.employee-table th:nth-child(4),
.employee-table td:nth-child(4) {
  width: 12%;
}
.employee-table th:nth-child(5),
.employee-table td:nth-child(5) {
  width: 15%;
}
.employee-table th:nth-child(6),
.employee-table td:nth-child(6) {
  width: 25%;
}
.employee-table th:nth-child(7),
.employee-table td:nth-child(7) {
  width: 10%;
}
.employee-table th:nth-child(8),
.employee-table td:nth-child(8) {
  width: 10%;
}

.employee-table tr:hover {
  background-color: #f1f1f1;
}

.edit,
.delete {
  float: left;
  width: 40px;
  padding: 5px 8px;
  font-size: 0.85em; /* Giảm kích thước chữ cho nút */
  border: none;
  border-radius: 3px;
  cursor: pointer;
  margin: 0 5px;
}

.edit {
  background-color: #28a745;
  color: white;
}

.delete {
  background-color: #e74c3c;
  color: white;
}

.edit:hover {
  background-color: #28a745;
}

.delete:hover {
  background-color: #c0392b;
}

/* Overlay */
.overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
}

/* Modal */
.modal {
  background-color: white;
  padding: 20px;
  border-radius: 5px;
  max-width: 880px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  text-align: center;
}
.modal label {
  display: block;
  font-weight: bold;
  color: rgb(255, 117, 140);
  text-align: left;
  margin-left: 2px;
}
.modal form {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
}

.input-row {
  display: flex;
  width: 100%;
}

.input-column {
  flex: 1;
  padding: 5px;
}

.input-column-full {
  width: 100%;
  padding: 5px;
}

.modal input,
.modal select {
  width: 400px;
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 3px;
  font-size: 1em;
}

.modal .input-column {
  margin-right: 10px;
}

.modal .input-column:last-child {
  margin-right: 0;
}

.modal h2 {
  margin-bottom: 20px;
  font-size: 1.5em;
}

.modal h3 {
  font-size: 24px;
  color: rgb(255, 117, 140);
}
.modal input {
  display: block;
  width: 400px;
  padding: 10px;
  margin-bottom: 10px;
  border: 1px solid #ddd;
  border-radius: 3px;
  font-size: 1em;
}

.modal button {
  padding: 10px 20px;
  margin: 5px;
  border: none;
  border-radius: 3px;
  cursor: pointer;
  font-size: 1em;
}

.modal button:first-of-type {
  background-color: #28a745;
  color: white;
}

.modal button:last-of-type {
  background-color: #e74c3c;
  color: white;
}

.modal button:hover:first-of-type {
  background-color: #28a745;
}

.modal button:hover:last-of-type {
  background-color: #c0392b;
}
/* Alert */
.alert {
  padding: 10px;
  margin: 10px 0;
  border-radius: 4px;
}
.alert-success {
  background-color: #d4edda;
  color: #155724;
  border: 1px solid #c3e6cb;
}
.alert-error {
  background-color: #f8d7da;
  color: #721c24;
  border: 1px solid #f5c6cb;
}

.confirm-modal {
  background-color: white;
  padding: 30px;
  border-radius: 8px;
  width: 400px;
  text-align: center;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  animation: modalFadeIn 0.3s ease;
}

@keyframes modalFadeIn {
  from {
    opacity: 0;
    transform: translateY(-20px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

.confirm-icon {
  font-size: 48px;
  color: #e74c3c;
  margin-bottom: 20px;
}

.confirm-modal h3 {
  color: #2c3e50;
  margin-bottom: 15px;
  font-size: 24px;
}

.confirm-modal p {
  color: #666;
  margin-bottom: 25px;
  font-size: 16px;
}

.confirm-actions {
  display: flex;
  justify-content: center;
  gap: 15px;
}

.confirm-btn,
.cancel-btn {
  padding: 10px 25px;
  border: none;
  border-radius: 5px;
  font-size: 16px;
  cursor: pointer;
  transition: background-color 0.2s;
}

.confirm-btn {
  background-color: #e74c3c;
  color: white;
}

.confirm-btn:hover {
  background-color: #c0392b;
}

.cancel-btn {
  background-color: #95a5a6;
  color: white;
}

.cancel-btn:hover {
  background-color: #7f8c8d;
}
.filter-group {
  display: flex;
  gap: 10px;
  align-items: center;
}

.position-filter {
  padding: 10px;
  border: 1px solid #bdc3c7;
  border-radius: 5px;
  outline: none;
  min-width: 150px;
  cursor: pointer;
}

.position-filter:focus {
  border-color: #3498db;
}
/* Thêm hiệu ứng cho overlay */
.overlay {
  animation: overlayFadeIn 0.3s ease;
}


@keyframes overlayFadeIn {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}
</style>
