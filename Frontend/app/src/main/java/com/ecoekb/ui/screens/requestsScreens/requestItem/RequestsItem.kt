package com.ecoekb.ui.screens.requestsScreens.requestItem

import androidx.compose.foundation.Image
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.foundation.verticalScroll
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.LocationOn
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.Stable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.graphics.painter.ColorPainter
import androidx.compose.ui.platform.LocalConfiguration
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import com.ecoekb.domain.models.RequestStatus
import com.ecoekb.ui.composeble.PersonalCard


@Composable
fun RequestsItem(
    requestId: String,
    viewModel: RequestItemViewModel = hiltViewModel()
) {
    val request = viewModel.getRequestById(requestId)

    var colorStatusBox = Color(0xFFfff389)
    if (request.status == RequestStatus().COMPLETE) {
        colorStatusBox = Color(0xFFd2ff89)
    }

    Column(
        modifier = Modifier
            .fillMaxSize()
            .background(MaterialTheme.colorScheme.background)
            .verticalScroll(rememberScrollState())
    ) {

        val w = LocalConfiguration.current.screenWidthDp.dp

        Image(
            painter = ColorPainter(Color.Blue),
            contentDescription = "Screen1",
            modifier = Modifier
                .size(w, w)
                .clip(
                    RoundedCornerShape(
                        bottomEnd = 15.dp,
                        bottomStart = 15.dp
                    )
                ),
//            contentScale = ContentScale.FillHeight
        )

        Row(
            modifier = Modifier
                .padding(16.dp)
                .fillMaxWidth(),
            verticalAlignment = Alignment.CenterVertically,
            horizontalArrangement = Arrangement.SpaceBetween
        ) {
            Text(
                text = request.title,
                style = MaterialTheme.typography.headlineMedium,
            )
            Box(
                modifier = Modifier
                    .clip(RoundedCornerShape(5.dp))
                    .background(MaterialTheme.colorScheme.onPrimary),
            ) {
                Text(
                    text = "Обращение №${request.numberRequest}",
                    style = MaterialTheme.typography.bodyMedium,
                    modifier = Modifier
                        .padding(8.dp)
                )
            }
        }

        Row(
            modifier = Modifier
                .padding(
                    bottom = 16.dp,
                    start = 16.dp,
                    end = 16.dp
                ),
            verticalAlignment = Alignment.CenterVertically,
        ) {
            Box(
                modifier = Modifier
                    .clip(RoundedCornerShape(5.dp))
                    .background(colorStatusBox)
            ) {
                Text(
                    text = request.status,
                    style = MaterialTheme.typography.bodyMedium,
                    modifier = Modifier
                        .padding(8.dp)
                )
            }

            Box(
                modifier = Modifier
                    .padding(start = 8.dp)
                    .clip(RoundedCornerShape(5.dp))
                    .background(MaterialTheme.colorScheme.onPrimary)
            ) {
                Text(
                    text = request.data,
                    style = MaterialTheme.typography.bodyMedium,
                    modifier = Modifier
                        .padding(8.dp)
                )
            }
        }

        Text(
            text = request.content,
            modifier = Modifier.padding(
                start = 16.dp,
                end = 16.dp
            )
        )

        Row(
            modifier = Modifier
                .padding(16.dp)
                .fillMaxWidth()
                .clip(RoundedCornerShape(5.dp))
                .background(MaterialTheme.colorScheme.onPrimary),
            verticalAlignment = Alignment.CenterVertically
        ){
            Icon(
                imageVector = Icons.Default.LocationOn,
                contentDescription = "",
                modifier = Modifier.padding(
                    start = 8.dp,
                    top = 8.dp,
                    bottom = 8.dp
                )
            )

            Text(
                text = request.location,
                style = MaterialTheme.typography.bodyMedium,
                modifier = Modifier
                    .padding(
                        start = 8.dp,
                        top = 8.dp,
                        bottom = 8.dp
                    )
            )
        }

        Text(
            text = "Сведения об обращающемся",
            style = MaterialTheme.typography.headlineMedium,
            modifier = Modifier
                .padding(start = 16.dp)
        )

        PersonalCard(
            modifier = Modifier
                .padding(16.dp)
                .fillMaxWidth()
                .clip(RoundedCornerShape(5.dp))
        )
    }
}

