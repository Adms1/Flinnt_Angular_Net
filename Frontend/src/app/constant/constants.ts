export const Constants = {
    CURRENT_PAGE: 1,
    ROWS_ON_PAGE: 10,
    HR_PAGE_ROWS_ON_PAGE: 100,
    YES: 'Yes',
    NO: 'No',
    TOKEN: 'access_token',
    REGEX: {
        EMAIL_PATTERN: new RegExp(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/),
        PASSWORD_PATTERN: new RegExp(
            /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{6,16}$/,
        ),
        PHONE_PATTERN: new RegExp(/^([0-9]{10,10})$/),
        DECIMAL_PATTRN: new RegExp(/^\d*?(\.\d{1,2})?$/),
        INTEGER_PATTERN: new RegExp(/^[0-9]*$/),
        CHARACTER_PATTERN: new RegExp(/^[a-zA-Z ]*$/),
        ZIP_PATTERN: new RegExp(/^[0-9]{5}(?:-[0-9]{4})?$/),
        LIMIT_PATTERN: new RegExp(/^([1-9]|1[0-9]|2[0])$/)
    },
    VALIDATION_MSG: {
        SIGN_UP: {
        },
    },
    USER_ADD_SUCCESS_MSG: 'User added sucessfully!',
    NO_INTERNET_CONNECTION_MSG: 'No internet connection!'
}

export const roleList = [
    { id: 1, role: 'Student' },
    { id: 2, role: 'Teacher' },
    { id: 3, role: 'Admin' },
]

export const bsConfig = {
    dateInputFormat: 'MM-DD-YYYY',
    containerClass: 'theme-blue',
    showWeekNumbers: false,
    selectFromOtherMonth: true
}

export const pageSizing = ['10', '25', '50', '100'];

export const phoneMask = { mask: '{+1} 000-000-0000' };