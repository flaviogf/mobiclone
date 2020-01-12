import 'package:bloc/bloc.dart';
import 'package:mobiclone/core/sign_up/email_repository.dart';
import 'package:mobiclone/pages/email/email_event.dart';
import 'package:mobiclone/pages/email/email_state.dart';

class EmailBloc extends Bloc<EmailEvent, EmailState> {
  final EmailRepository _emailRepository;

  EmailBloc(this._emailRepository);

  @override
  EmailState get initialState => InitiatedEmailState();

  @override
  Stream<EmailState> mapEventToState(EmailEvent event) async* {
    if (event is EmailEventSubmit) {
      yield SubmittedEmailState(event.value);

      if (event.value.isEmpty) {
        yield RequiredEmailState(event.value, 'The email must be informed.');
      } else {
        final String email = await _emailRepository.addEmail(event.value);
        yield ValidatedEmailState(email);
      }
    }
  }
}
