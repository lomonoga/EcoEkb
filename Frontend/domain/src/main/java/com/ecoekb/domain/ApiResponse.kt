package com.ecoekb.domain

sealed class ApiResponse<T>(
    data: T? = null,
    exception: Exception? = null
) {
    class Success<T>(val data: T) : ApiResponse<T>(data, null)
    class Error<T>(exception: Exception) : ApiResponse<T>(null, exception)
}