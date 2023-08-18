package com.ecoekb.networkdata.network

object Urls {

    const val BASE_URL: String = "http://45.153.68.211:80/"

    const val REGISTRATION: String = BASE_URL + "api/user/add-user"
    const val GET_ACCESS_TOKEN: String = BASE_URL + "api/auth/login"

    const val GET_ALL_PETITIONS: String = BASE_URL + "api/petition/get-all-petitions"
    const val GET_PETITION_BY_ID: String = BASE_URL + "api/petition/get-petition-by-id"
    const val PUT_ADD_PETITION: String = BASE_URL + "api/petition/add-petition"

}